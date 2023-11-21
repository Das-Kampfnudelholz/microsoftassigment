using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.NetworkInformation;
using WebApp1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<TodoItem> Items { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
             _httpClientFactory = httpClientFactory;
        }

    public async Task OnGetAsync()
{
    var _httpClient = _httpClientFactory.CreateClient("MyClient");
    if (!string.IsNullOrEmpty(SearchString))
    { 
        // Fix the filter in the following code block
        var queryString = $"api/TodoItems?userName={SearchString}&page=1&pageSize=10";
        using HttpResponseMessage filteredResponse = await _httpClient.GetAsync(queryString);
        var filteredResults = JsonConvert.DeserializeObject<PaginatedTodo>(await filteredResponse.Content.ReadAsStringAsync());
        Items = filteredResults.Items;

    }
    else
    {
        var queryString = "api/TodoItems?page=1&pageSize=10";
        using HttpResponseMessage response = await _httpClient.GetAsync(queryString);
        var results = JsonConvert.DeserializeObject<PaginatedTodo>(await response.Content.ReadAsStringAsync());
        Items = results.Items;
    }
}

    }
}