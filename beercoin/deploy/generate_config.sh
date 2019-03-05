#!/bin/sh

./beercoin generate-template --validators-count=5 common.toml

./beercoin generate-config common.toml pub_1.toml sec_1.toml --peer-address 1.1.1.1:6331
./beercoin generate-config common.toml pub_2.toml sec_2.toml --peer-address 2.2.2.2:6332
./beercoin generate-config common.toml pub_3.toml sec_3.toml --peer-address 3.3.3.3:6333
./beercoin generate-config common.toml pub_4.toml sec_4.toml --peer-address 4.4.4.4:6334
./beercoin generate-config common.toml pub_5.toml sec_5.toml --peer-address 5.5.5.5:6335

./beercoin finalize --public-api-address 0.0.0.0:8200 --private-api-address 0.0.0.0:8091 sec_1.toml node_1_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml pub_4.toml pub_5.toml
./beercoin finalize --public-api-address 0.0.0.0:8201 --private-api-address 0.0.0.0:8092 sec_2.toml node_2_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml pub_4.toml pub_5.toml
./beercoin finalize --public-api-address 0.0.0.0:8202 --private-api-address 0.0.0.0:8093 sec_3.toml node_3_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml pub_4.toml pub_5.toml
./beercoin finalize --public-api-address 0.0.0.0:8203 --private-api-address 0.0.0.0:8094 sec_4.toml node_4_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml pub_4.toml pub_5.toml
./beercoin finalize --public-api-address 0.0.0.0:8204 --private-api-address 0.0.0.0:8095 sec_5.toml node_5_cfg.toml --public-configs pub_1.toml pub_2.toml pub_3.toml pub_4.toml pub_5.toml
