use crate::{
    config::*,
    schema::BeerCoinSchema,
    transactions::BeerCoinTransactions::{self, TxPay, TxIssue},
    wallet::Wallet,
};
use exonum::{
    api::{self, ServiceApiBuilder, ServiceApiState},
    blockchain::{self, BlockProof, Transaction, TransactionSet},
    crypto::Hash,
    helpers::Height,
    node::TransactionSend,
    storage::{ListProof, Snapshot},
};
use std::cmp::Reverse;

#[derive(Debug, Clone)]
pub struct BeerCoinApi;

impl BeerCoinApi {
    pub fn get_wallet(state: &ServiceApiState, query: WalletQuery) -> api::Result<Wallet> {
        let snapshot = state.snapshot();
        let schema = BeerCoinSchema::new(snapshot);
        schema
            .wallet(query.id)
            .ok_or_else(|| api::Error::NotFound("\"Wallet not found\"".to_string()))
    }

    pub fn wallet_info(state: &ServiceApiState, query: WalletQuery) -> api::Result<WalletInfo> {
        let snapshot = state.snapshot();
        let general_schema = blockchain::Schema::new(&snapshot);
        let beercoin_schema = BeerCoinSchema::new(&snapshot);

        let max_height = general_schema.block_hashes_by_height().len() - 1;
        let block_proof = general_schema
            .block_and_precommits(Height(max_height))
            .unwrap();

        let wallet = beercoin_schema.wallet(query.id);
        let wallet_history = wallet.map(|_| {
            let history = beercoin_schema.wallet_history(query.id);
            let proof = history.get_range_proof(0, history.len());

            let transactions: Vec<BeerCoinTransactions> = history
                .iter()
                .map(|record| general_schema.transactions().get(&record).unwrap())
                .map(|raw| BeerCoinTransactions::tx_from_raw(raw).unwrap())
                .collect::<Vec<_>>();

            WalletHistory {
                proof,
                transactions,
            }
        });

        Ok(WalletInfo {
            block_proof,
            wallet_history,
        })
    }

    pub fn report(state: &ServiceApiState, query: ReportQuery) -> api::Result<Report> {
        let snapshot = state.snapshot();
        let schema = BeerCoinSchema::new(&snapshot);
        let general_schema = blockchain::Schema::new(&snapshot);

        let mined = schema.wallet(ISSUER_ID).map(|x| ISSUER_BALLANCE - x.balance()).unwrap_or(0);
        let shop = schema.wallet(SHOP_ID).map(|x| x.balance()).unwrap_or(0);

        let (top_rich, top_buyers) = Self::tops(query.top, &schema, &general_schema);

        let log = Self::log(query.tx_count, &general_schema);

        let report = Report {
            coins_total: ISSUER_BALLANCE,
            coins_mined: mined,
            coins_spent: shop,
            top_rich,
            top_buyers,
            log,
        };
        Ok(report)
    }

    pub fn post_transaction(
        state: &ServiceApiState,
        query: BeerCoinTransactions,
    ) -> api::Result<TransactionResponse> {
        let transaction: Box<dyn Transaction> = query.into();
        let tx_hash = transaction.hash();

        state.sender().send(transaction)?;
        Ok(TransactionResponse { tx_hash })
    }

    pub fn wire(builder: &mut ServiceApiBuilder) {
        builder
            .public_scope()
            .endpoint("v1/wallet", Self::get_wallet)
            .endpoint("v1/report", Self::report)
            .endpoint("v1/wallets/info", Self::wallet_info)
            .endpoint_mut("v1/wallets/transfer", Self::post_transaction);
    }

    fn tops(
        top: usize,
        schema: &BeerCoinSchema<&Box<Snapshot>>,
        general_schema: &blockchain::Schema<&Box<Snapshot>>,
    ) -> (Vec<Wallet>, Vec<Buyer>) {
        let mut wallets =
            schema.wallets().values().filter(|x| x.id() > 0).collect::<Vec<_>>();
        let limit = std::cmp::min(top, wallets.len());

        wallets.sort_by_key(|x| Reverse(x.balance()));
        let top_rich = wallets[..limit].to_vec();

        let mut buyers = wallets
            .iter()
            .map(|x| {
                let spent = schema
                    .wallet_history(x.id())
                    .iter()
                    .flat_map(|record| general_schema.transactions().get(&record))
                    .flat_map(|raw| BeerCoinTransactions::tx_from_raw(raw))
                    .fold(0_u64, |acc, tx| match tx {
                        TxPay(x) => acc + x.amount(),
                        _ => acc,
                    });
                Buyer { buyer: x.clone(), spent }
            })
            .collect::<Vec<_>>();
        buyers.sort_by_key(|x| Reverse(x.spent));
        let top_buyers = buyers[..limit].to_vec();

        (top_rich, top_buyers)
    }

    fn log(
        tx_count: usize,
        general_schema: &blockchain::Schema<&Box<Snapshot>>,
    ) -> Vec<TransactionLog> {
        let Height(zero) = Height::zero();
        let Height(height) = general_schema.height();

        (zero..=height).rev()
            .flat_map(|height| general_schema.block_transactions(Height(height))
                .iter()
                .flat_map(move |hash| general_schema.transactions().get(&hash)
                    .and_then(|raw| BeerCoinTransactions::tx_from_raw(raw).ok()
                        .map(|tx| {
                            let success = general_schema
                                .transaction_results()
                                .get(&hash)
                                .and_then(|result| result.ok())
                                .is_some();
                            TransactionLog::new(height, hash, tx, success)
                        })))
                .collect::<Vec<_>>())
            .take(tx_count)
            .collect::<Vec<_>>()
    }
}

#[derive(Debug, Serialize, Deserialize, Clone, Copy)]
pub struct WalletQuery {
    pub id: i64,
}

#[derive(Debug, Serialize, Deserialize, Clone, Copy)]
pub struct ReportQuery {
    pub top: usize,
    pub tx_count: usize,
}

#[derive(Debug, Serialize, Deserialize, Clone)]
pub struct Buyer {
    pub buyer: Wallet,
    pub spent: u64,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct TransactionLog {
    pub block: u64,
    pub tx_hash: Hash,
    pub id: i64,
    pub seed: u64,
    pub message_id: u64,
    pub amount: u64,
    pub success: bool,
}

impl TransactionLog {
    fn new(block: u64, tx_hash: Hash, tx: BeerCoinTransactions, success: bool) -> Self {
        match tx {
            TxIssue(x) => {
                Self {
                    block,
                    tx_hash,
                    id: x.id(),
                    seed: x.seed(),
                    message_id: 0,
                    amount: ISSUE_AMOUNT,
                    success,
                }
            }
            TxPay(x) => {
                Self {
                    block,
                    tx_hash,
                    id: x.id(),
                    seed: x.seed(),
                    message_id: 1,
                    amount: x.amount(),
                    success,
                }
            }
        }
    }
}

#[derive(Debug, Serialize, Deserialize)]
pub struct Report {
    pub coins_total: u64,
    pub coins_mined: u64,
    pub coins_spent: u64,
    pub top_rich: Vec<Wallet>,
    pub top_buyers: Vec<Buyer>,
    pub log: Vec<TransactionLog>,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct TransactionResponse {
    pub tx_hash: Hash,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct WalletHistory {
    pub proof: ListProof<Hash>,
    pub transactions: Vec<BeerCoinTransactions>,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct WalletInfo {
    pub block_proof: BlockProof,
    pub wallet_history: Option<WalletHistory>,
}
