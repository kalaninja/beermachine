use exonum::{
    crypto::Hash,
    storage::{Fork, MapIndex, ProofListIndex, Snapshot},
};
use crate::wallet::Wallet;

#[derive(Debug)]
pub struct BeerCoinSchema<T> {
    view: T,
}

impl<T> BeerCoinSchema<T> where T: AsRef<dyn Snapshot> {
    pub fn new(view: T) -> Self {
        BeerCoinSchema { view }
    }

    pub fn wallets(&self) -> MapIndex<&T, i64, Wallet> {
        MapIndex::new("beercoin.wallets", &self.view)
    }

    pub fn wallet_history(&self, id: i64) -> ProofListIndex<&T, Hash> {
        ProofListIndex::new_in_family("beercoin.wallet_history", &id, &self.view)
    }

    pub fn wallet(&self, id: i64) -> Option<Wallet> {
        self.wallets().get(&id)
    }


    pub fn state_hash(&self) -> Vec<Hash> {
        vec![]
    }
}

impl<'a> BeerCoinSchema<&'a mut Fork> {
    pub fn wallets_mut(&mut self) -> MapIndex<&mut Fork, i64, Wallet> {
        MapIndex::new("beercoin.wallets", &mut self.view)
    }

    pub fn wallet_history_mut(&mut self, id: i64) -> ProofListIndex<&mut Fork, Hash> {
        ProofListIndex::new_in_family("beercoin.wallet_history", &id, &mut self.view)
    }

    pub fn transfer(&mut self, sender: Wallet, receiver: Wallet, amount: u64, transaction: &Hash) {
        let s = {
            let mut history = self.wallet_history_mut(sender.id());
            history.push(*transaction);
            let history_hash = history.merkle_root();
            sender.decrease_balance(amount, &history_hash)
        };
        let r = {
            let mut history = self.wallet_history_mut(receiver.id());
            history.push(*transaction);
            let history_hash = history.merkle_root();
            receiver.increase_balance(amount, &history_hash)
        };

        println!("Transfer {} between wallets: {:?} => {:?}", amount, sender, receiver);

        let mut wallets = self.wallets_mut();
        wallets.put(&s.id(), s);
        wallets.put(&r.id(), r);
    }
}
