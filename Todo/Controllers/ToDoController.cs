using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    public class ToDoController(AppDbContext _context) : Controller
    {
        // GET: ToDoController
        public async Task<IActionResult> Index()
        {
            var toDoItems = await _context.TodoItems.ToListAsync();
            return View(toDoItems);
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            return View("Upsert");
        }

        // POST: ToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItem toDoItem)
        {
            if (!ModelState.IsValid)
                return View("Upsert", toDoItem);

            _context.TodoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ToDoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var toDoItem = await _context.TodoItems.FindAsync(id);

            if(toDoItem == null)
            {
                return NotFound();
            }

            return View("Upsert", toDoItem);
        }

        // POST: ToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoItem toDoItem)
        {
            if(id != toDoItem.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View("Upsert", toDoItem);

            _context.TodoItems.Update(toDoItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: ToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var toDoItem = await _context.TodoItems.FindAsync(id);

            if(toDoItem == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
