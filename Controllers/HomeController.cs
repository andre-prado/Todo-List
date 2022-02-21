using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public List<TodoModel> Get([FromServices] AppDbContext context)
        {
            return context.Todos.ToList();
        }

        public TodoModel Post([FromServices] AppDbContext context, [FromBody] TodoModel todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }
    }
}