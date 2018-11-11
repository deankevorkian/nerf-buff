using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerfBuff.Models;

namespace NerfBuff.Controllers
{
    public class CommentsController : Controller
    {
        private readonly masterContext _context = new masterContext();

        // GET: Comments
        public IActionResult Index(string Author, string Content)
        {
            var masterContext = _context.Comments.Include(c => c.Post).ToList();

            if (!string.IsNullOrEmpty(Author))
            {
                masterContext = masterContext.Where(c => c.Author.ToUpper().Contains(Author.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(Content))
            {
                masterContext = masterContext.Where(c => c.Content.ToUpper().Contains(Content.ToUpper())).ToList();
            }
            masterContext.OrderBy((x) => x.Date);

            return View(masterContext);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var comments = await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(comments);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Author,Content,Date")] Comments comments)
        {
            comments.Author = comments.Author == null ? "Anon" : comments.Author;
            comments.Date = DateTime.Now;
            if (CommentsExists(comments.Id))
            {
                var z = _context.Comments.Select((x) => x.Id).OrderByDescending((y) => y).ToList();
                comments.Id = z[0] + 1;
            }
            _context.Add(comments);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(Comments comment)
        {
            if (!HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                return RedirectToAction("Login", "Users");
            }

            comment.Author = System.Text.Encoding.UTF8.GetString(userName);
            comment.Date = DateTime.Now;
            if (CommentsExists(comment.Id))
            {
                var z = _context.Comments.Select((x) => x.Id).OrderByDescending((y) => y).ToList();
                comment.Id = z[0] + 1;
            }
            _context.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", comments.PostId);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,Title,Author,Content,Date")] Comments comments)
        {
            if (id != comments.Id)
            {
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsExists(comments.Id))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", comments.PostId);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var comments = await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
