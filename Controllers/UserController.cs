using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using learn_dotnet.Data;

namespace learn_dotnet.Controllers
{
    public class UserController : Controller
    {
        private readonly PizzashopCContext _context;

        public UserController(PizzashopCContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var pizzashopCContext = _context.Users.Include(u => u.City).Include(u => u.Country).Include(u => u.Role).Include(u => u.State);
            return View(await pizzashopCContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.City)
                .Include(u => u.Country)
                .Include(u => u.Role)
                .Include(u => u.State)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid");
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid");
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            ViewData["Stateid"] = new SelectList(_context.States, "Stateid", "Stateid");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Email,Roleid,Firstname,Lastname,Username,Profilephoto,Countryid,Stateid,Cityid,Address,Zipcode,Phonenumber,Isactive,Isdeleted,Createdat,Modifiedat,Createdby,Modifiedby")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid", user.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", user.Countryid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", user.Roleid);
            ViewData["Stateid"] = new SelectList(_context.States, "Stateid", "Stateid", user.Stateid);
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid", user.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", user.Countryid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", user.Roleid);
            ViewData["Stateid"] = new SelectList(_context.States, "Stateid", "Stateid", user.Stateid);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Email,Roleid,Firstname,Lastname,Username,Profilephoto,Countryid,Stateid,Cityid,Address,Zipcode,Phonenumber,Isactive,Isdeleted,Createdat,Modifiedat,Createdby,Modifiedby")] User user)
        {
            if (id != user.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Userid))
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
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid", user.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", user.Countryid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", user.Roleid);
            ViewData["Stateid"] = new SelectList(_context.States, "Stateid", "Stateid", user.Stateid);
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.City)
                .Include(u => u.Country)
                .Include(u => u.Role)
                .Include(u => u.State)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'PizzashopCContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Userid == id)).GetValueOrDefault();
        }
    }
}
