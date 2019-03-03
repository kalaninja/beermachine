use exonum::crypto::Hash;
use crate::config::*;

encoding_struct! {
    struct Wallet {
        id:             i64,
        balance:        u64,
        history_len:    u64,
        history_hash:   &Hash,
    }
}

impl Wallet {
    pub fn increase_balance(&self, amount: u64, history_hash: &Hash) -> Self {
        Self::new(
            self.id(),
            self.balance() + amount,
            self.history_len() + 1,
            history_hash,
        )
    }

    pub fn decrease_balance(&self, amount: u64, history_hash: &Hash) -> Self {
        Self::new(
            self.id(),
            self.balance() - amount,
            self.history_len() + 1,
            history_hash,
        )
    }

    pub fn create(id: i64) -> Self {
        Wallet::new(id, 0, 0, &Hash::default())
    }

    pub fn issuer() -> Self {
        Wallet::new(ISSUER_ID, ISSUER_BALLANCE, 0, &Hash::default())
    }

    pub fn shop() -> Self {
        Wallet::new(SHOP_ID, 0, 0, &Hash::default())
    }
}