using System;
using System.Collections.Generic;

namespace NerfBuff.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime? Date { get; set; }

        public Posts Post { get; set; }
    }
}
