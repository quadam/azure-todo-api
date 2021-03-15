namespace TodoApi.Models
{
    public class TodoModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Group { get; set; }
        public bool Completed { get; set; }
    }
}
