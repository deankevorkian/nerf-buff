using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.Math.Distances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NerfBuff.Models;

namespace NerfBuff.Controllers
{
    public class Location
    {
        public double Long;
        public double Lat;
        public string EventName;
    }
    public class EventsController : Controller
    {
        private readonly masterContext _context = new masterContext();

        [AllowAnonymous]
        public IActionResult EventsLocations()
        {
            var EventsLocations =
                from Event in _context.Events
                select new Location
                {
                    Long = Event.Long,
                    Lat = Event.Lat,
                    EventName = Event.Title
                };

            return Json(EventsLocations.ToList());
        }


        // GET: Events
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.TryGetValue("IsAdmin", out var userJson))
            {
                ViewBag.IsAdmin = System.Text.Encoding.UTF8.GetString(userJson) == "True";
            }
            else
            {
                ViewBag.IsAdmin = false;
            }

            var recEvent = CalcRecommendedEventForUser();
            ViewBag.RecommendedEvent = recEvent;
            return View(await _context.Events.ToListAsync());
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Time,Long,Lat,Author")] Events events)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Time,Long,Lat,Author")] Events events)
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
            _context.Events.Remove(events);
            //TODO: add delete event to user
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        public IActionResult CurrentEvents()
        {
            return View();
        }

        private Events CalcRecommendedEventForUser()
        {
            const int kNeighbors = 1;

            var knn = new KNearestNeighbors<double[]>(kNeighbors, distance: new Accord.Math.Distances.SquareEuclidean());

            if (!HttpContext.Session.TryGetValue("UserName", out var userNameNotEncoded))
            {
                return null;
            }

            var userName = System.Text.Encoding.UTF8.GetString(userNameNotEncoded);

            var usersEvents = _context.EventToUser.Include(evToUser => evToUser.Event).Include(evToUser => evToUser.EventUserNameNavigation).OrderBy(s => s.EventUserNameNavigation.BlogUserAge);

            LinkedList<double[]> usersAge = new LinkedList<double[]>();
            LinkedList<int> eventIds = new LinkedList<int>();

            foreach (var userEvent in usersEvents.OrderBy(userEv => userEv.EventId))
            {
                usersAge.AddLast(new double[] { Convert.ToDouble(userEvent.EventUserNameNavigation.BlogUserAge) });
                eventIds.AddLast(userEvent.EventId);
            }

            var inputs = usersAge.Select(user => user.ToArray()).ToArray();
            var outputs = eventIds.ToArray();

            knn.Learn(inputs, outputs);

            var currUserObj = _context.Users.First(users => users.BlogUserName == userName);

            var currUserAge = new double[] { currUserObj.BlogUserAge };

            var decision = knn.Decide(currUserAge);

            var decidedEvent = _context.Events.First(someEvent => someEvent.Id == decision);
            return decidedEvent;
        }
    }
}
