using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Models;
using Sibintek.BeerMachine.Settings;
using Polly;
using Sibintek.BeerMachine.Services;

namespace Sibintek.BeerMachine.BlockchainClient
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

        private ConcurrentDictionary<int, bool> _nodeStatus = new ConcurrentDictionary<int, bool>();
        private int _currentNodeIndex;

        public BlockсhainClient(BlockchainOptions options, IHttpClientFactory httpClientFactory)
        {
            _options = options;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransactionStatus> GetStatus(string transactionHash)
        {
            var node = GetNodeIndex();
            return await Policy
                .Handle<Exception>()
                .RetryAsync(4, (ex, i) =>
                {
                    _nodeStatus.AddOrUpdate(node, false, (_, __) => false);
                    node = node >= _options.NodeUrls.Length - 1 ? 0 : node + 1;
                })
                .ExecuteAsync(async () =>
                {
                    var url = $"{_options.NodeUrls[node]}/api/explorer/v1/transactions?hash={transactionHash}";

                    using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            return null;
                        }

                        var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                        var result =
                            JsonConvert.DeserializeObject<TransactionStatus>(responseText, _serializerSettings);

                        _nodeStatus.AddOrUpdate(node, true, (_, __) => true);
                        return result;
                    }
                });
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
            var node = GetNodeIndex();
            return await Policy
                .Handle<Exception>()
                .RetryAsync(4, (ex, i) =>
                {
                    _nodeStatus.AddOrUpdate(node, false, (_, __) => false);
                    node = node >= _options.NodeUrls.Length - 1 ? 0 : node + 1;
                })
                .ExecuteAsync(async () =>
                {
                    var url = $"{_options.NodeUrls[node]}/api/services/beercoin/v1/wallets/transfer";

                    var requestText = JsonConvert.SerializeObject(request, _serializerSettings);
                    var content = new StringContent(requestText, Encoding.UTF8, "application/json");

                    using (var response = await _httpClientFactory.CreateClient().PostAsync(url, content))
                    {
                        var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                        var result =
                            JsonConvert.DeserializeObject<TransactionResponse>(responseText, _serializerSettings);

                        _nodeStatus.AddOrUpdate(node, true, (_, __) => true);
                        return result;
                    }
                });
        }

        public async Task<Wallet> GetWallet(long walletId)
        {
            var node = GetNodeIndex();
            return await Policy
                .Handle<Exception>()
                .RetryAsync(4, (ex, i) =>
                {
                    _nodeStatus.AddOrUpdate(node, false, (_, __) => false);
                    node = node >= _options.NodeUrls.Length - 1 ? 0 : node + 1;
                })
                .ExecuteAsync(async () =>
                {
                    var url = $"{_options.NodeUrls[node]}/api/services/beercoin/v1/wallet?id={walletId}";

                    using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            return null;
                        }

                        var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Wallet>(responseText, _serializerSettings);

                        _nodeStatus.AddOrUpdate(node, true, (_, __) => true);
                        return result;
                    }
                });
        }

        public async Task<BlockchainReportResponse> Report(int count = 10)
        {
            var node = GetNodeIndex();
            return await Policy
                .Handle<Exception>()
                .RetryAsync(4, (ex, i) =>
                {
                    _nodeStatus.AddOrUpdate(node, false, (_, __) => false);
                    node = node >= _options.NodeUrls.Length - 1 ? 0 : node + 1;
                })
                .ExecuteAsync(async () =>
                {
                    var url = $"{_options.NodeUrls[node]}/api/services/beercoin/v1/report?top={count}&tx_count={count}";

                    using (var response = await _httpClientFactory.CreateClient().GetAsync(url))
                    {
                        var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                        var result =
                            JsonConvert.DeserializeObject<BlockchainReportResponse>(responseText, _serializerSettings);

                        _nodeStatus.AddOrUpdate(node, true, (_, __) => true);
                        result.NodeIndex = node;
                        return result;
                    }
                });
        }

        public IDictionary<int, bool> NodeStatuses() => _nodeStatus;

        private int GetNodeIndex()
        {
            var last = _options.NodeUrls.Length - 1;
            if (last == Interlocked.CompareExchange(ref _currentNodeIndex, 0, last))
            {
                return _currentNodeIndex;
            }

            Interlocked.Increment(ref _currentNodeIndex);
            return _currentNodeIndex;
        }
    }
}