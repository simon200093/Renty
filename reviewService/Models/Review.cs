using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reviewService.Models
{
    public class Review
    {
        public long Id { get; set; }

        public int ReviewGrade { get; set; }

        public string ReviewContent { get; set; }

        public DateTime ReviewTime { get; set; }

        public long AccountId { get; set; }

        public long OfferId { get; set; }
    }
}
