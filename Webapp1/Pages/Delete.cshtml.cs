using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace WebApp1.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;                
        }

        public async Task OnGetAsync(int? id)
        {
            var _httpClient = _httpClientFactory.CreateClient("MyClient");
            if (id == null)
            {
                throw new Exception();
            }
            
            using HttpResponseMessage response = await _httpClient.DeleteAsync($"api/TodoItems/{id}");

            // Use ViewData to store the status code for displaying it in the view
            ViewData["StatusCode"] = (int)response.StatusCode;


        }
    }
}