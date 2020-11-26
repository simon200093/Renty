using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reviewService.Models;

namespace reviewService.Data
{
    public class reviewServiceContext : DbContext
    {
        public reviewServiceContext (DbContextOptions<reviewServiceContext> options)
            : base(options)
        {
        }

        public DbSet<reviewService.Models.Review> Review { get; set; }
    }
}
