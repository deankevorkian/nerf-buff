using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class EventToUser
    {
        public int EventId { get; set; }
        public string EventUserName { get; set; }

        public Events Event { get; set; }
        public Users EventUserNameNavigation { get; set; }
    }
}
