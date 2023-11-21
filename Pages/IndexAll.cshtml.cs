using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using WebApp1.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp1.Pages
{
    public class IndexAllModel : PageModel
    {
        public List<TodoItem> Items { get; set; }
        public string NameSort { get; set; }
        public string UpnSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        private readonly IConfiguration Configuration;
        public PaginatedList<TodoItem> PaginatedItems { get; set; }
		private readonly IHttpClientFactory _httpClientFactory;


		public IndexAllModel(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            Configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync(string sortOrder, int? pageIndex)
        {
			var _httpClient = _httpClientFactory.CreateClient("MyClient");
			using HttpResponseMessage response = await _httpClient.GetAsync("api/TodoItems?page=1&pageSize=20");
            var results = JsonConvert.DeserializeObject<PaginatedTodo>(await response.Content.ReadAsStringAsync());
            Items = results.Items.ToList();
            
            var pageSize = 20;
            PaginatedItems = await PaginatedList<TodoItem>.CreateAsync(Items.AsQueryable(), pageIndex ?? 1, pageSize);
        }
    }
}
