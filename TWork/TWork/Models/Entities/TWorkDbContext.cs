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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<TEAM> TEAMs { get; set; }
        public DbSet<TASK> TASKs { get; set; }
        public DbSet<COMMENT> COMMENTs { get; set; }
        public DbSet<USER_TEAM> USERS_TEAMs { get; set; }
        public DbSet<USER_TEAM_ROLES> USER_TEAM_ROLEs { get; set; }
        public DbSet<TASK_STATUS> TASK_STATUSes { get; set; }
        public DbSet<ROLE> ROLEs { get; set; }
        public DbSet<MESSAGE> MESSAGEs { get; set; }
        public DbSet<MESSAGE_TYPE> MESSAGE_TYPEs { get; set; }
    }
}
