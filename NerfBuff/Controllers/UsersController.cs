using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerfBuff.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NerfBuff.Controllers
{
    public class UsersController : Controller
    {
        private readonly masterContext _context = new masterContext();

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.TryGetValue("UserName", out var userJson))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("BlogUserName", "BlogUserPassword")]Users userToBeLogged)
        {
            if (ModelState.IsValid)
            {
                // Check if the user login details are ok
                var result = await _context.Users.FirstOrDefaultAsync(user => user.BlogUserName == userToBeLogged.BlogUserName && user.BlogUserPassword == userToBeLogged.BlogUserPassword);

                if (result == null)
                {
                    ModelState.AddModelError("", "The Username or Password you entered are incorrect. Please check your info and try again.");
                    return View();
                }
                
                HttpContext.Session.SetString("UserName", result.BlogUserName.Trim());
                HttpContext.Session.SetString("IsAdmin", result.BlogIsAdmin.ToString());

                return RedirectToAction("Index", "Home", null);
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("IsAdmin");

            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not view" });
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.BlogUserName == id);
            if (users == null)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not view" });
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogUserName,BlogUserPassword,BlogUserAge")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.BlogIsAdmin = false;

                _context.Add(users);
                await _context.SaveChangesAsync();

                return await Login(users);
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not edit" });
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not edit" });
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BlogUserName,BlogUserPassword,BlogIsAdmin,BlogUserAge")] Users users)
        {
            ModelState.Remove("BlogUserName");

            if (id != users.BlogUserName)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not edit" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.BlogUserName))
                    {
                        return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not edit" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not delete" });
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.BlogUserName == id);
            if (users == null)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { error = "user does not exist - could not delete" });
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.BlogUserName == id);
        }
    }
}
