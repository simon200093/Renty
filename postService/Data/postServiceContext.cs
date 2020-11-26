using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using postService.Models;

namespace postService.Data
{
    public class postServiceContext : DbContext
    {

        public postServiceContext(DbContextOptions<postServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Post { get; set; }
    }
}
