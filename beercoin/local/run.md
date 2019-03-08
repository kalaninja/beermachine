ulimit -Sn unlimited
export RUST_LOG="info"
./beercoin run --node-config ./node_1_cfg.toml --db-path ./db1 --public-api-address 0.0.0.0:8200

ulimit -Sn unlimited
export RUST_LOG="info"
./beercoin run --node-config ./node_2_cfg.toml --db-path ./db2 --public-api-address 0.0.0.0:8201

ulimit -Sn unlimited
export RUST_LOG="info"
./beercoin run --node-config ./node_3_cfg.toml --db-path ./db3 --public-api-address 0.0.0.0:8202