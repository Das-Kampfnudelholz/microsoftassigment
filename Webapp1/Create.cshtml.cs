using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
        public TodoItem Item { get; set; } = new TodoItem();

        public async Task<IActionResult> OnPostAsync()
        {
            var _httpClient = _httpClientFactory.CreateClient("MyClient");

            if (!ModelState.IsValid || Item == null)
            {
                return Page();
            }

            try
            {
                // Complete the logic to post a new todolist item
                using var content = new StringContent(JsonConvert.SerializeObject(Item), Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await _httpClient.PostAsync("api/TodoItems", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    // Handle the error case, e.g., display an error message
                    ModelState.AddModelError(string.Empty, "Failed to create the todo item. Status code: " + response.StatusCode);
                    return Page();
                }
            }
            catch (HttpRequestException)
            {
                // Handle the exception if there is an issue with the HTTP request
                ModelState.AddModelError(string.Empty, "Failed to connect to the API.");
                return Page();
            }
        }
    }
}