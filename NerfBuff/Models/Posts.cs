using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? Date { get; set; }
        public string Content { get; set; }
    }
}
