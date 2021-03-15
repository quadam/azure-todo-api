using Microsoft.Azure.Cosmos.Table;

namespace TodoApi.Entities
{
    public class TodoEntity : TableEntity
    {
        public string Content { get; set; }
        public bool Completed { get; set; }
    }
}
