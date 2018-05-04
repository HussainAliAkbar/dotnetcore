using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using System;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]

    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
                _context.TodoItems.Add(new TodoItem {Name = "Item2"});
                _context.SaveChanges();
            }
        }

    [HttpGet]
    public ActionResult GetAll()
    {
        System.Console.WriteLine("hello");
        return Ok(_context.TodoItems.ToList()) ;
    }

    [HttpGet("{id}", Name = "GetTodo")]
    public ActionResult GetById(long id)
    {
        var item = _context.TodoItems.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TodoItem item)
    {
        Console.WriteLine(item);
        _context.TodoItems.Add(item);
        _context.SaveChanges();

        return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody] long id, TodoItem item)
    {

        System.Console.WriteLine(item.IsComplete);
        var todo = _context.TodoItems.Find(id);


        System.Console.WriteLine("name: ", todo.Name);

        System.Console.WriteLine(todo.IsComplete);
        if (todo == null)
        {
            return NotFound();
        }

        todo.IsComplete = item.IsComplete;
        todo.Name = item.Name;

        _context.TodoItems.Update(todo);
        _context.SaveChanges();
        return NoContent();
    }       
    }
}