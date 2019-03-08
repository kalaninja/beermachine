using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Settings;

namespace Sibintek.BeerMachine.Services
{
    public class BlockсhainClient : IBlockсhainClient
    {
        private readonly BlockchainOptions _options;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public BlockсhainClient(BlockchainOptions options, IHttpClientFactory httpClientFactory)
        {
            _options = options;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransactionStatus> GetStatus(string transactionHash)
        {
            var url = _options.NodeUrls[0] + $"/api/explorer/v1/transactions?hash={transactionHash}";

            using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TransactionStatus>(responseText, _serializerSettings);
            }
        }

        public Task<TransactionResponse> Pay(long walletId, long sum)
        {
            var request = new TransactionRequest(walletId.ToString(), DateTime.Now.Ticks.ToString(), 1, sum);
            return Transfer(request);
        }

        public Task<TransactionResponse> Issue(long walletId)
        {
            var request = new TransactionRequest(walletId.ToString(), DateTime.Now.Ticks.ToString(), 0);
            return Transfer(request);
        }

        private async Task<TransactionResponse> Transfer(TransactionRequest request)
        {
            var url = _options.NodeUrls[0] + $"/api/services/beercoin/v1/wallets/transfer";

            var requestText = JsonConvert.SerializeObject(request, _serializerSettings);
            var content = new StringContent(requestText, Encoding.UTF8, "application/json");

            using (var response = await _httpClientFactory.CreateClient().PostAsync(url, content))
            {
                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TransactionResponse>(responseText, _serializerSettings);
            }
        }

        public async Task<Wallet> GetWallet(long walletId)
        {
            var url = _options.NodeUrls[0] + $"/api/services/beercoin/v1/wallet?id={walletId}";

            using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Wallet>(responseText, _serializerSettings);
            }
        }

        public async Task<BlockchainReportResponse> Report(int count = 10)
        {
            var url = _options.NodeUrls[0] + $"/api/services/beercoin/v1/report?top={count}&tx_count={count}";

            using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
            {
                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<BlockchainReportResponse>(responseText, _serializerSettings);
            }
        }
    }

    public class TransactionRequest
    {
        public TransactionBody Body { get; }

        public int ProtocolVersion => 0;

        public int ServiceId => 26;

        public short MessageId { get; set; }

        public string Signature =>
            "2c234680adaa67f1e6573895f1557230ea5373b0972f8aa714611f78931c4bae49680580d41ac806977a7a4f9556781018f1061c9be4adcaabc3760c5a92a70b";

        public TransactionRequest(string id, string seed, short messageId, long? amount = null)
        {
            Body = new TransactionBody
            {
                Id = id,
                Seed = seed,
                Amount = amount?.ToString()
            };
            MessageId = messageId;
        }
    }

    public class TransactionBody
    {
        public string Id { get; set; }

        public string PubKey => "6ce29b2d3ecadc434107ce52c287001c968a1b6eca3e5a1eb62a2419e2924b85";

        public string Seed { get; set; }

        public string Amount { get; set; }
    }

    public class TransactionStatus
    {
        public string Type { get; set; }
        
        public StatusModel Status { get; set; }

        public bool IsSuccess => Type == "committed" && Status?.Type == "success";

        public bool IsError => Status?.Type == "error";

        public class StatusModel
        {
            public string Type { get; set; }
        }
    }

    public class MockBlockсhainClient : IBlockсhainClient
    {
        public Task<TransactionStatus> GetStatus(string transactionHash)
        {
            return Task.FromResult<TransactionStatus>(new TransactionStatus {Type = "committed"});
        }

        public Task<TransactionResponse> Pay(long walletId, long sum)
        {
            return Task.FromResult(new TransactionResponse {TxHash = "ashdfkyasoi62934yp9gwfd87fsc"});
        }

        public Task<TransactionResponse> Issue(long walletId)
        {
            return Task.FromResult(new TransactionResponse() {TxHash = "ashdfkyasoi62934yp9gwfd87fsc"});
        }

        public Task<Wallet> GetWallet(long id)
        {
            return Task.FromResult(new Wallet
            {
                Id = new Random().Next(1, 11),
                Balance = new Random().Next(15, 101)
            });
        }

        public Task<BlockchainReportResponse> Report(int count = 10)
        {
            var random = new Random();

            return Task.FromResult(new BlockchainReportResponse()
            {
                TopRich = Enumerable.Range(1, 10).Select(x => new Wallet
                {
                    Id = x,
                    Balance = random.Next(15, 101)
                }).ToList(),

                TopBuyers = Enumerable.Range(1, 10).Select(x => new Buyer
                {
                    BuyerWallet = new Wallet
                    {
                        Id = x,
                        Balance = random.Next(15, 101)
                    },
                    Spent = random.Next(16, 200)
                }).ToList(),

                CoinsMined = random.Next(100, 2503),
                CoinsSpent = random.Next(272, 1273),
                CoinsTotal = random.Next(123, 1204),
                Log = Enumerable.Range(1, 11).Select(x => new TransactionLogResponse
                {
                    Amount = random.Next(0, 200),
                    Id = x,
                    Block = random.Next(1000, 20002)
                }).ToList()
            });
        }
    }
}