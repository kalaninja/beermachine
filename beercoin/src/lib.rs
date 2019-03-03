#[macro_use]
extern crate exonum;
#[macro_use]
extern crate failure;
extern crate serde;
#[macro_use]
extern crate serde_derive;

pub mod api;
pub mod config;
pub mod schema;
pub mod transactions;
pub mod wallet;

pub const BEERCOIN_SERVICE_ID: u16 = 26;
pub const BEERCOIN_SERVICE_NAME: &str = "beercoin";

use exonum::{api::ServiceApiBuilder,
             blockchain::{Service, Transaction, TransactionSet},
             crypto::Hash,
             encoding::Error as EncodingError,
             helpers::fabric::{self, Context},
             messages::RawTransaction,
             storage::Snapshot,
};

use crate::{
    schema::BeerCoinSchema,
    transactions::BeerCoinTransactions,
};

#[derive(Default, Debug)]
pub struct BeerCoinService;

impl Service for BeerCoinService {
    fn service_id(&self) -> u16 {
        BEERCOIN_SERVICE_ID
    }

    fn service_name(&self) -> &str {
        BEERCOIN_SERVICE_NAME
    }

    fn state_hash(&self, view: &dyn Snapshot) -> Vec<Hash> {
        let schema = BeerCoinSchema::new(view);
        schema.state_hash()
    }

    fn tx_from_raw(&self, raw: RawTransaction) -> Result<Box<dyn Transaction>, EncodingError> {
        BeerCoinTransactions::tx_from_raw(raw).map(Into::into)
    }

    fn wire_api(&self, builder: &mut ServiceApiBuilder) {
        api::BeerCoinApi::wire(builder);
    }
}

#[derive(Debug)]
pub struct ServiceFactory;

impl fabric::ServiceFactory for ServiceFactory {
    fn service_name(&self) -> &str {
        BEERCOIN_SERVICE_NAME
    }

    fn make_service(&mut self, _: &Context) -> Box<dyn Service> {
        Box::new(BeerCoinService)
    }
}
