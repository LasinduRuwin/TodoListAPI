using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListAPI.DTOs;
using TodoListAPI.Models;
using TodoListAPI.Repositories;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDTO>> GetAll()
        {
            var todoItems = todoRepository.GetAll();
            var todoItemsDTO = todoItems.Select(t => new TodoItemDTO
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted
            });

            return Ok(todoItemsDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItemDTO> GetById(int id)
        {
            var todoItem = todoRepository.GetById(id);
            if (todoItem == null)
                return NotFound();

            var todoItemDTO = new TodoItemDTO
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            };

            return Ok(todoItemDTO);
        }

        [HttpPost]
        public ActionResult<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Title = todoItemDTO.Title,
                IsCompleted = todoItemDTO.IsCompleted
            };

            var createdItem = todoRepository.Add(todoItem);
            todoItemDTO.Id = createdItem.Id;

            return CreatedAtAction(nameof(GetById), new { id = todoItemDTO.Id }, todoItemDTO);
        }

        [HttpPut()]
        public IActionResult Update(TodoItemDTO todoItemDTO)
        {

            var todoItem = new TodoItem
            {
                Id = todoItemDTO.Id,
                Title = todoItemDTO.Title,
                IsCompleted = todoItemDTO.IsCompleted
            };

            var result = todoRepository.Update(todoItem);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = todoRepository.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
