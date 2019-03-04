extern crate exonum;
extern crate exonum_testkit;
#[macro_use]
extern crate serde_json;

use exonum::{
    api::node::public::explorer::TransactionQuery,
    crypto::{self, CryptoHash, Hash},
};
use exonum_testkit::{ApiKind, TestKit, TestKitApi, TestKitBuilder};
use beercoin::{
    BeerCoinService,
    api::{WalletQuery, Report, ReportQuery},
    config::*,
    transactions::{TxIssue, TxPay},
    wallet::Wallet,
};

#[test]
fn test_transfer() {
    let (mut testkit, api) = create_testkit();
    let id = 1000;

    {
        let tx = api.issue(id);
        testkit.create_block();
        api.assert_tx_status(tx.hash(), &json!({ "type": "success" }));

        let wallet = api.get_wallet(id).unwrap();
        assert_eq!(wallet.balance(), ISSUE_AMOUNT);
        let issuer = api.get_wallet(ISSUER_ID).unwrap();
        assert_eq!(issuer.balance(), ISSUER_BALLANCE - ISSUE_AMOUNT);
    }

    {
        let amount = ISSUE_AMOUNT;
        let tx = api.pay(id, amount);
        testkit.create_block();
        api.assert_tx_status(tx.hash(), &json!({ "type": "success" }));

        let wallet = api.get_wallet(id).unwrap();
        assert_eq!(wallet.balance(), ISSUE_AMOUNT - amount);
        let issuer = api.get_wallet(ISSUER_ID).unwrap();
        assert_eq!(issuer.balance(), ISSUER_BALLANCE - ISSUE_AMOUNT);
        let shop = api.get_wallet(SHOP_ID).unwrap();
        assert_eq!(shop.balance(), amount);
    }

    {
        let current_balance = api.get_wallet(id).unwrap().balance();
        let current_shop = api.get_wallet(SHOP_ID).unwrap().balance();
        let amount = current_balance + 1;
        let tx = api.pay(id, amount);
        testkit.create_block();
        api.assert_tx_status(
            tx.hash(),
            &json!({ "type": "error", "code": 0, "description": "Insufficient currency amount" }),
        );

        let wallet = api.get_wallet(id).unwrap();
        assert_eq!(wallet.balance(), current_balance);
        let issuer = api.get_wallet(ISSUER_ID).unwrap();
        assert_eq!(issuer.balance(), ISSUER_BALLANCE - ISSUE_AMOUNT);
        let shop = api.get_wallet(SHOP_ID).unwrap();
        assert_eq!(shop.balance(), current_shop);
    }
}

#[test]
fn test_transfer_no_wallet() {
    let (mut testkit, api) = create_testkit();
    let tx = api.pay(1111, 1000);
    testkit.create_block();
    api.assert_tx_status(
        tx.hash(),
        &json!({ "type": "error", "code": 0, "description": "Insufficient currency amount" }),
    );
}

#[test]
fn test_report() {
    let (mut testkit, api) = create_testkit();
    (0..3).for_each(|_| { api.issue(1); });
    (0..7).for_each(|_| { api.issue(2); });
    (0..7).for_each(|_| { api.issue(3); });
    testkit.create_block();

    api.pay(1, 1);
    api.pay(2, 2);
    api.pay(3, 3);
    testkit.create_block();

    let report = api.report();
    assert_eq!(report.coins_mined, 17);
    assert_eq!(report.coins_spent, 6);

    let top_rich = report.top_rich.iter().map(|x| x.id()).collect::<Vec<i64>>();
    assert_eq!(top_rich, vec![2, 3, 1])
}

struct BeerCoinApi {
    pub inner: TestKitApi,
}

impl BeerCoinApi {
    fn get_wallet(&self, id: i64) -> Option<Wallet> {
        self.inner
            .public(ApiKind::Service("beercoin"))
            .query(&WalletQuery { id })
            .get("v1/wallet")
            .ok()
    }

    fn issue(&self, id: i64) -> TxIssue {
        let (pubkey, key) = crypto::gen_keypair();
        let tx = TxIssue::new(&pubkey, id, 0, &key);

        let tx_info: serde_json::Value = self.inner
            .public(ApiKind::Service("beercoin"))
            .query(&tx)
            .post("v1/wallets/transfer")
            .unwrap();
        assert_eq!(tx_info, json!({ "tx_hash": tx.hash() }));

        tx
    }

    fn pay(&self, id: i64, amount: u64) -> TxPay {
        let (pubkey, key) = crypto::gen_keypair();
        let tx = TxPay::new(&pubkey, id, amount, 0, &key);

        let tx_info: serde_json::Value = self.inner
            .public(ApiKind::Service("beercoin"))
            .query(&tx)
            .post("v1/wallets/transfer")
            .unwrap();
        assert_eq!(tx_info, json!({ "tx_hash": tx.hash() }));

        tx
    }

    fn report(&self) -> Report {
        self.inner
            .public(ApiKind::Service("beercoin"))
            .query(&ReportQuery { top: 10 })
            .get("v1/report")
            .unwrap()
    }

    fn assert_tx_status(&self, tx_hash: Hash, expected_status: &serde_json::Value) {
        let info: serde_json::Value = self.inner
            .public(ApiKind::Explorer)
            .query(&TransactionQuery::new(tx_hash))
            .get("v1/transactions")
            .unwrap();

        if let serde_json::Value::Object(mut info) = info {
            let tx_status = info.remove("status").unwrap();
            assert_eq!(tx_status, *expected_status);
        } else {
            panic!("Invalid transaction info format, object expected");
        }
    }
}

fn create_testkit() -> (TestKit, BeerCoinApi) {
    let testkit = TestKitBuilder::validator()
        .with_service(BeerCoinService)
        .create();
    let api = BeerCoinApi { inner: testkit.api() };
    (testkit, api)
}