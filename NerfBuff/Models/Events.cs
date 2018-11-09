﻿using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class Events
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Time { get; set; }
        public string Location { get; set; }
        public string Author { get; set; }
    }
}