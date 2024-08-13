using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public interface ITodoRepository
    {
        TodoItem Add(TodoItem todoItem);
        bool Delete(int id);
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        bool Update(TodoItem todoItem);
    }
}