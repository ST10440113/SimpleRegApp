using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleRegApp.Models;

namespace SimpleRegApp.Data
{
    public class SimpleRegAppContext : DbContext
    {
        public SimpleRegAppContext (DbContextOptions<SimpleRegAppContext> options)
            : base(options)
        {
        }

        public DbSet<SimpleRegApp.Models.Events> Events { get; set; } = default!;
        public DbSet<SimpleRegApp.Models.Account> Account { get; set; } = default!;
    }
}
