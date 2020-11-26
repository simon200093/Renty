using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offerService.Models
{
    public class Offer
    {
        public long Id { get; set; }

        public DateTime OfferTime { get; set; }

        public string OfferPrice { get; set; }

        public string OfferStatus { get; set; }

        public long AccountId { get; set; }

        public long PostId { get; set; }
    }
}
