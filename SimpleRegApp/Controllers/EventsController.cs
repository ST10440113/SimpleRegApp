using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SimpleRegApp.Data;
using SimpleRegApp.Models;


namespace SimpleRegApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly SimpleRegAppContext _context;

        public EventsController(SimpleRegAppContext context)
        {
            _context = context;
        }

        // GET: Events

       
        public async Task<IActionResult> Index(string searchString)
        {
            var events = from e in _context.Events
                         select e;
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Type.Contains(searchString));
                return View(await events.ToListAsync());
            }
            return View(await _context.Events.ToListAsync());
        }

        
        public async Task<IActionResult> UserIndex(string searchString)
        {
            var events = from e in _context.Events
                         select e;
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Type.Contains(searchString));
                return View(await events.ToListAsync());
            }
            return View(await _context.Events.ToListAsync());
        }

        [HttpGet]public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? userName, string? password)
        {
            var user = await _context.Account.FirstOrDefaultAsync(u => u.Username == userName && u.Password == password);

            if (string.IsNullOrEmpty(userName) && (string.IsNullOrEmpty(password)))
            {
                TempData["Error"] = "Login details missing";
            }

                if (user == null)

                {
                    TempData["Error"] = "Invalid Login";
                    return View();
                }
                else 
                {
                    return RedirectToAction(nameof(Index));
                }


            }
        [HttpGet]public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string?username, string?password,string? firstName,string? lastName,string? email)
        {
            if(!string.IsNullOrEmpty(username) && (!string.IsNullOrEmpty(password)))
            {
                TempData["Error"] = "Please complete all fields for registration";
            }
            var existingUsername = _context.Account.FirstOrDefault(eu => eu.Username == username );
            var existingPassword = _context.Account.FirstOrDefault( ee => ee.Password == password);

            if (existingUsername != null){

                TempData["Error"] = "Username already exists";
                return View();
            }
            else if (existingPassword != null)
            {
                    TempData["Error"] = "Password already exists";
                    return View();
            }
            else
            {
                var newUser = new Account
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Username = username,
                    Password = password,
                    Role = "User"
                };
                _context.Account.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
        }



        // GET: Events/Details/5
      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }
        
        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,Date,Description,Type")] Events events, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                events.ImageUrl = "/images/" + file.FileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,Date,Description,Type")] Events events)
        {
            if (id != events.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var events = await _context.Events.FindAsync(id);
            if (events != null)
            {
                _context.Events.Remove(events);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
