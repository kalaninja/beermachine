using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Settings;

namespace Sibintek.BeerMachine.Services
{
    public class BlockсhainClient : IBlockсhainClient
    {
        private readonly BlockchainOptions _options;

        private readonly IHttpClientFactory _httpClientFactory;

        public BlockсhainClient(BlockchainOptions options, IHttpClientFactory httpClientFactory)
        {
            _options = options;

            _httpClientFactory = httpClientFactory;
        }

        public Task<TransactionResponse> Pay(long walletId, decimal sum)
        {
            var body = "";

            var content = new StringContent(body, Encoding.UTF8, "application/json");

            return Transfer(content);
        }

        public Task<TransactionResponse> Issue(long walletId)
        {
            var body = "";

            var content = new StringContent(body, Encoding.UTF8, "application/json");

            return Transfer(content);
        }

        private async Task<TransactionResponse> Transfer(StringContent content)
        {
            var url = _options.NodeUrl + $"/v1/wallets/transfer";

            using (var response = await _httpClientFactory.CreateClient().PostAsync(url, content))
            {
                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TransactionResponse>(responseText);
            }
        }

        public async Task<Wallet> GetWallet(long walletId)
        {
            var url = _options.NodeUrl + $"/v1/wallet?walletId={walletId}";

            using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Wallet>(responseText);
            }
        }

        public async Task<BlockchainReportResponse> Report(int count = 10)
        {
            var url = _options.NodeUrl + $"/v1/report?top={count}";

            using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
            {
                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<BlockchainReportResponse>(responseText);
            }
        }

        public Task<List<TransactionInfo>> TransactionLog(int count = 10)
        {
            throw new NotImplementedException();
        }
    }

    public class MockBlockсhainClient : IBlockсhainClient
    {
        public Task<TransactionResponse> Pay(long walletId, decimal sum)
        {
            return Task.FromResult(new TransactionResponse {Hash = "ashdfkyasoi62934yp9gwfd87fsc"});
        }

        public Task<TransactionResponse> Issue(long walletId)
        {
            return Task.FromResult(new TransactionResponse() {Hash = "ashdfkyasoi62934yp9gwfd87fsc"});
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

                TopBuyers = Enumerable.Range(1, 10).Select(x => new Wallet
                {
                    Id = x,
                    Balance = random.Next(15, 101)
                }).ToList(),

                CoinsMined = random.Next(100, 2503),
                CoinsSpent = random.Next(272, 1273),
                CoinsTotal = random.Next(123, 1204)
            });
        }

        public Task<List<TransactionInfo>> TransactionLog(int count = 10)
        {
            var random = new Random();

            var transactions = Enumerable.Range(1, 11).Select(x => new TransactionInfo
            {
                WalletId = x,
                Sum = random.Next(0, 100),
                Type = (TransactionType) random.Next(0, 2),
                TransactionDate = DateTime.Now
            }).ToList();

            return Task.FromResult(transactions);
        }
    }
}