using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class EventToUser
    {
        public int EventId { get; set; }
        public string UserName { get; set; }

        public Events Event { get; set; }
        public Users UserNameNavigation { get; set; }
    }
}
