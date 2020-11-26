using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postService.Models
{
    public class Post
    {
        public long Id { get; set; }

        public string PostType { get; set; }

        public string PostContent { get; set; }

        public DateTime PostTime { get; set; }

        public string Post_price { get; set; }

        public long AccountId { get; set; }

    }
}
