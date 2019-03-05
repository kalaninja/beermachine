using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sibintek.BeerMachine.Domain;
using Sibintek.BeerMachine.Settings;

namespace Sibintek.BeerMachine.Services
{
    public class BlockсhainClient : IBlockсhainClient
    {
        private readonly BlockchainOptions _options;

        private readonly HttpClient _httpClient;

        public BlockсhainClient(BlockchainOptions options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }
        
        public Task<TransactionResponse> Pay(long walletId, decimal sum)
        {
            var body = "";
            
            var content = new StringContent(body, Encoding.UTF8,"application/json");

            return Transfer(content);
        }

        public Task<TransactionResponse> Issue(long walletId)
        {
            var body = "";
            
            var content = new StringContent(body, Encoding.UTF8,"application/json");
            
            return Transfer(content);
        }

        private async Task<TransactionResponse> Transfer(StringContent content)
        {
            var url = _options.NodeUrl + $"/v1/wallets/transfer";
            
            using (var response = await _httpClient.PostAsync(url, content))
            {
                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TransactionResponse>(responseText);
            }
        }

        public async Task<Wallet> GetWallet(long walletId)
        {
            var url = _options.NodeUrl + $"/v1/wallet?walletId={walletId}";
            
            using (var response =  await _httpClient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Wallet>(responseText);
            }
        }

        public async Task<BlockchainReport> Report(int count = 10)
        {
            var url = _options.NodeUrl + $"/v1/report?top={count}";
            
            using (var response =  await _httpClient.GetAsync(url))
            {
                var responseText = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<BlockchainReport>(responseText);
            }
        }
    }
}