using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public class TWorkDbContext : IdentityDbContext<USER>
    {
        public TWorkDbContext(DbContextOptions<TWorkDbContext> options) : base(options)
        { }

        public DbSet<TEAM> Teams { get; set; }
        public DbSet<TASK> Tasks { get; set; }
        public DbSet<COMMENT> Comments { get; set; }
    }
}
