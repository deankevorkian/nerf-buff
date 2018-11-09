using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NerfBuff.Models
{
    public class StatsController : Controller
    {
        private masterContext _context = new masterContext();

        public IActionResult Index()
        {
            Stats Charts = new Stats
            {
                PostsCountByMonth = _context.Posts.GroupBy(x => x.Date.Month).
                Select(c => new { Month = c.Key, Count = c.Count() }).
                ToDictionary(c => CultureInfo.CurrentCulture.DateTimeFormat.
                GetAbbreviatedMonthName(c.Month), c => c.Count),

                CommentsCountByPosts = _context.Comments.GroupBy(c => c.Author).
                OrderByDescending(c => c.Count()).
                Select(c => new { Author = c.Key, Count = c.Count() }).
                Take(5).
                ToDictionary(c => c.Author, c => c.Count)
            };

            return View(Charts);
        }
    }
}