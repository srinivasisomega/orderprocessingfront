using System.Text.Json;
using System.Text;

namespace youyou
{
    public class DurableFunctionsService : IDurableFunctionsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _functionsBaseUrl = " http://localhost:7193";

        public DurableFunctionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> StartOrderAsync(OrderPayload order)
        {
            var payload = JsonSerializer.Serialize(order);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_functionsBaseUrl}/api/OrderProcessingOrchestration_HttpStart", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string>(responseContent);
        }

        public async Task<string?> GetOrderStatusAsync(string instanceId)
        {
            var response = await _httpClient.GetAsync($"{_functionsBaseUrl}/runtime/webhooks/durabletask/instances/{instanceId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
        }
    }

}
