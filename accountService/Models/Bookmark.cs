using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace accountService
{
    public class Bookmark
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long PostId { get; set; }
    }
}
