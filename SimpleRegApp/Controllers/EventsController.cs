using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public async Task<IActionResult> UserIndex(string searchString)
        {
            var events = from e in _context.Events
                         select e;
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Type.Contains(searchString) || e.EventName.Contains(searchString));
            }
            var eventList = await events.ToListAsync();

            if (!eventList.Any())
            {
                TempData["Error"] = "Sorry, no events match your search";
            }

            return View(eventList);
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Events.ToListAsync());
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var users = from u in _context.Account
                        select u;
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString) || u.LastName.Contains(searchString));
                return View(await users.ToListAsync());
            }
            return View(await _context.Account.ToListAsync());
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? userName, string? password, string? role)
        {
            var user = await _context.Account.FirstOrDefaultAsync(u => u.Username == userName && u.Password == password && u.Role == role);

            if (string.IsNullOrEmpty(userName) && (string.IsNullOrEmpty(password)) && (string.IsNullOrEmpty(role)))
            {
                TempData["Error"] = "Login details missing";
            }
            if (user == null)

            {
                TempData["Error"] = "Account does not exist";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

            }
            if (user.Role == "Admin")
            {
                return RedirectToAction(nameof(Index));
            }
            else if (user.Role == "User")
            {
                return RedirectToAction(nameof(UserIndex));
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Successfully logged out!";
            return RedirectToAction(nameof(UserIndex));
        }





        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string? username, string? password, string? firstName, string? lastName, string? email)
        {
            if (!string.IsNullOrEmpty(username) && (!string.IsNullOrEmpty(password)))
            {
                TempData["Error"] = "Please complete all fields for registration";
            }
            var existingUsername = _context.Account.FirstOrDefault(eu => eu.Username == username);
            var existingPassword = _context.Account.FirstOrDefault(ee => ee.Password == password);

            if (existingUsername != null)
            {

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
                TempData["Success"] = "Registration successful";
                return RedirectToAction(nameof(UserIndex));


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
                .FirstOrDefaultAsync(m => m.EventId == id);
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
        public async Task<IActionResult> Create([Bind("Id,EventName,Date,Description,Type")] Events events)
        {

            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Events/Edit/5
        [HttpGet]public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,Date,Description,Type")] Events events)
        {
            if (id != events.EventId)
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
                    if (!EventsExists(events.EventId))
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
        public async Task<IActionResult> RegisterDetails(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.RegisteredEvents
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        public async Task<IActionResult> EventRegistration(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventRegistration(int id,[Bind("FName,LName,EmailAddress,PhoneNumber")] RegisteredEvents re)
        {
            var eventToRegister = await _context.Events.FindAsync(id);
            if (eventToRegister == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                re.EventId = id;
                Console.WriteLine($"re.Id = {re.Id}, re.EventId = {re.EventId}");
                _context.Add(re);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RegisterDetails), new { id = re.Id });
            }

            return View(eventToRegister);
        }


        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
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
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
