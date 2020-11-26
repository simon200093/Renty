using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using accountService.Models;
using accountService;

namespace accountService.Data
{
    public class accountServiceContext : DbContext
    {
        public accountServiceContext (DbContextOptions<accountServiceContext> options)
            : base(options)
        {
        }

        public DbSet<accountService.Models.Account> Account { get; set; }

        public DbSet<accountService.Bookmark> Bookmark { get; set; }
    }
}
