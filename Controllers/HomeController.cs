using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        AppDbContext _context;
        public HomeController([FromServices] AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<TodoModel> Get()
        {
            return _context.Todos.ToList();
        }

        public TodoModel Post([FromBody] TodoModel todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return todo;
        }
    }
}