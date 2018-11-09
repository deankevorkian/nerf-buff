using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace NerfBuff.Models
{
    public class Stats
    {
        public Dictionary<string, int> PostsCountByMonth;

        public Dictionary<string, int> CommentsCountByPosts;
    }
}
