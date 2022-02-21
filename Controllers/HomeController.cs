using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("/todo")]
    public class HomeController : ControllerBase
    {
        AppDbContext _context;
        public HomeController([FromServices] AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(_context.Todos.ToList());


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoModel todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return Created($"/{todo.Id}", todo);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] TodoModel todoUpdated)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound();


            todo.Done = todoUpdated.Done;
            todo.Title = todoUpdated.Title;

            _context.Update(todo);
            _context.SaveChanges();

            return Ok(todo);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
                return NotFound();

            _context.Remove(todo);
            _context.SaveChanges();

            return Ok(todo);
        }
    }
}