using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.Models
{
    public class RecordDbContext:DbContext
    {
        public RecordDbContext(DbContextOptions<RecordDbContext> options) : base(options)
        {
        }

        public DbSet<Parent> parents { get; set; }
        public DbSet<Child> children { get; set; }
    }
}
