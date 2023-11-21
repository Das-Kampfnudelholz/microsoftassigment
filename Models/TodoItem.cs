using Newtonsoft.Json;

namespace WebApp1.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? AssignedUserUpn { get; set; }
        public bool IsComplete { get; set; }
    }

    public class PaginatedTodo
    {
        public required List<TodoItem> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
