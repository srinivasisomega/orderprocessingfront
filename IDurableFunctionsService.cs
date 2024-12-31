using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace youyou
{
    
    public interface IDurableFunctionsService
    {
        Task<string> StartOrderAsync(OrderPayload order);
        Task<string?> GetOrderStatusAsync(string instanceId);
    }
}
