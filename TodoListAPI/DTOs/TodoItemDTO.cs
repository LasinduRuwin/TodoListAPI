namespace TodoListAPI.DTOs
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
