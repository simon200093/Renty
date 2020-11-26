using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using offerService.Models;

namespace offerService.Data
{
    public class offerServiceContext : DbContext
    {
        public offerServiceContext (DbContextOptions<offerServiceContext> options)
            : base(options)
        {
        }

        public DbSet<offerService.Models.Offer> Offer { get; set; }
    }
}
