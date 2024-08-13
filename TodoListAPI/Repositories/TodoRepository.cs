using System.Xml.Linq;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> todoItems;

        public TodoRepository()
        {
            this.todoItems = new List<TodoItem>();
        }

        public IEnumerable<TodoItem> GetAll() => todoItems;

        public TodoItem? GetById(int id) => todoItems.FirstOrDefault(t => t.Id == id);

        public TodoItem Add(TodoItem todoItem)
        {
            todoItem.Id = todoItems.Count + 1;
            todoItems.Add(todoItem);
            return todoItem;
        }

        public bool Update(TodoItem todoItem)
        {
            var existingItem = GetById(todoItem.Id);
            if (existingItem == null) return false;

            existingItem.Title = todoItem.Title;
            existingItem.IsCompleted = todoItem.IsCompleted;
            return true;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item == null) return false;

            todoItems.Remove(item);
            return true;
        }
    }
}
