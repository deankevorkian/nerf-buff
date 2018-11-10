using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class Events
    {
        public Events()
        {
            EventToUser = new HashSet<EventToUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Author { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }

        public ICollection<EventToUser> EventToUser { get; set; }
    }
}
