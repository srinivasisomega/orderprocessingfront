using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace youyou.Pages
{
   
    public class OrderStatusModel : PageModel
    {
        private readonly IDurableFunctionsService _durableFunctionsService;

        public OrderStatusModel(IDurableFunctionsService durableFunctionsService)
        {
            _durableFunctionsService = durableFunctionsService;
        }

        public string? Status { get; private set; }

        public async Task OnGetAsync(string instanceId)
        {
            Status = await _durableFunctionsService.GetOrderStatusAsync(instanceId);
        }
    }

}
