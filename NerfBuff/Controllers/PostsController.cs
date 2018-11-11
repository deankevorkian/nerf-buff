using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerfBuff.Models;
using Newtonsoft.Json;

namespace NerfBuff.Controllers
{
    public class PostsController : Controller
    {
        private masterContext _context = new masterContext();

        // GET: Posts
        public IActionResult Index(string Title, string Author, string Content)
        {
            if (HttpContext.Session.TryGetValue("IsAdmin", out var userJson))
            {
                ViewBag.IsAdmin = System.Text.Encoding.UTF8.GetString(userJson) == "True";
            }
            else
            {
                ViewBag.IsAdmin = false;
            }

            var masterContext = _context.Posts.Include(c => c.Comments).ToList();
            if (!string.IsNullOrEmpty(Title))
            {
                masterContext = _context.Posts.Where(c => c.Title.ToUpper().Contains(Title.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Author))
            {
                masterContext = _context.Posts.Where(c => c.Author.ToUpper().Contains(Author.ToUpper()) || c.Comments.Any((y) => y.Author.ToUpper().Contains(Author.ToUpper()))).ToList();
            }

            if (!string.IsNullOrEmpty(Content))
            {
                masterContext = _context.Posts.Where(c => c.Content.ToUpper().Contains(Content.ToUpper()) || c.Comments.Any((y) => y.Content.ToUpper().Contains(Content.ToUpper()))).ToList();
            }

            return View(masterContext);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var posts = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Date,Content")] Posts posts)
        {
            int z = !_context.Posts.Any() ? 1 : _context.Posts.Max(x => x.Id);
            posts.Id = z + 1;
            
            if (!HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                return RedirectToAction("Login", "Users");
            }

            posts.Author = System.Text.Encoding.UTF8.GetString(userName);
            _context.Add(posts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Date,Content")] Posts posts)
        {
            if (id != posts.Id)
            {
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.Id))
                    {
                        return RedirectToAction("Error", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(posts);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var posts = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posts = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
