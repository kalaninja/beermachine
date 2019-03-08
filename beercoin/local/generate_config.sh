#!/bin/sh

./beercoin generate-template --validators-count=3 common.toml

./beercoin generate-config common.toml pub_1.toml sec_1.toml --peer-address 127.0.0.1:6331
./beercoin generate-config common.toml pub_2.toml sec_2.toml --peer-address 127.0.0.1:6332
./beercoin generate-config common.toml pub_3.toml sec_3.toml --peer-address 127.0.0.1:6333

./beercoin finalize --public-api-address 0.0.0.0:8200 --private-api-address 0.0.0.0:8091 sec_1.toml node_1_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml
./beercoin finalize --public-api-address 0.0.0.0:8201 --private-api-address 0.0.0.0:8092 sec_2.toml node_2_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml
./beercoin finalize --public-api-address 0.0.0.0:8202 --private-api-address 0.0.0.0:8093 sec_3.toml node_3_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml
