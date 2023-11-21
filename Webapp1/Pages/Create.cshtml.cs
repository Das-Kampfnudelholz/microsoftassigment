using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using WebApp1.Models;

namespace WebApp1.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
             _httpClientFactory = httpClientFactory;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoItem Item { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var _httpClient = _httpClientFactory.CreateClient("MyClient");
            
            if (!ModelState.IsValid || Item == null)
            {
                return Page();
            }
            //Complete the logic to post a new todolist item
            

            return RedirectToPage("./Index");
        }
    }
}
