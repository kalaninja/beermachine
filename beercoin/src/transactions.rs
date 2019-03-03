use exonum::{
    blockchain::{ExecutionError, ExecutionResult, Transaction},
    crypto::{CryptoHash, Hash, PublicKey},
    storage::Fork,
};

use crate::{
    BEERCOIN_SERVICE_ID,
    config::*,
    schema::BeerCoinSchema,
    wallet::Wallet,
};

transactions! {
    pub BeerCoinTransactions{
        const SERVICE_ID = BEERCOIN_SERVICE_ID;

        struct TxIssue {
            pub_key: &PublicKey,
            id: i64,
            seed: u64
        }

        struct TxPay {
            pub_key: &PublicKey,
            id: i64,
            amount: u64,
            seed: u64
        }
    }
}

impl Transaction for TxIssue {
    fn verify(&self) -> bool {
//        self.verify_signature(self.pub_key())
        true
    }

    fn execute(&self, view: &mut Fork) -> ExecutionResult {
        let mut schema = BeerCoinSchema::new(view);
        let id = self.id();
        let hash = self.hash() as Hash;
        let issuer = schema.wallet(ISSUER_ID).unwrap_or(Wallet::issuer());
        let receiver = schema.wallet(id).unwrap_or(Wallet::create(id));

        if issuer.balance() < ISSUE_AMOUNT {
            Err(Error::InsufficientCurrencyAmount)?
        }

        schema.transfer(issuer, receiver, ISSUE_AMOUNT, &hash);
        Ok(())
    }
}

impl Transaction for TxPay {
    fn verify(&self) -> bool {
//        self.verify_signature(self.pub_key())
        true
    }

    fn execute(&self, view: &mut Fork) -> ExecutionResult {
        let mut schema = BeerCoinSchema::new(view);
        let id = self.id();
        let hash = self.hash() as Hash;
        let sender = schema.wallet(id).ok_or(Error::InsufficientCurrencyAmount)?;
        let shop = schema.wallet(SHOP_ID).unwrap_or(Wallet::shop());
        let amount = self.amount();

        if sender.balance() < amount {
            Err(Error::InsufficientCurrencyAmount)?
        }

        schema.transfer(sender, shop, amount, &hash);
        Ok(())
    }
}

#[derive(Debug, Fail)]
#[repr(u8)]
pub enum Error {
    #[fail(display = "Insufficient currency amount")]
    InsufficientCurrencyAmount = 0,
}

impl From<Error> for ExecutionError {
    fn from(value: Error) -> ExecutionError {
        let description = value.to_string();
        ExecutionError::with_description(value as u8, description)
    }
}
