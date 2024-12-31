using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace youyou.Pages
{
   
    public class IndexModel : PageModel
    {
        private readonly IDurableFunctionsService _durableFunctionsService;

        public IndexModel(IDurableFunctionsService durableFunctionsService)
        {
            _durableFunctionsService = durableFunctionsService;
        }

        [BindProperty]
        public OrderPayload Order { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var instanceId = await _durableFunctionsService.StartOrderAsync(Order);
            TempData["InstanceId"] = instanceId;
            return RedirectToPage("/OrderStatus", new { instanceId });
        }
    }

}
