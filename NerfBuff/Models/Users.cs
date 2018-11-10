using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class Users
    {
        public Users()
        {
            EventToUser = new HashSet<EventToUser>();
        }

        public string BlogUserName { get; set; }
        public string BlogUserPassword { get; set; }
        public bool BlogIsAdmin { get; set; }
        public int BlogUserAge { get; set; }

        public ICollection<EventToUser> EventToUser { get; set; }
    }
}
