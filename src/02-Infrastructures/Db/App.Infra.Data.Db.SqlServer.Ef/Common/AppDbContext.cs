

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.Entities;




namespace App.Infra.Data.Db.SqlServer.Ef.Common
{
    public class AppDbContext : IdentityDbContext<UserTa, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TaskU> TaskUs { get; set; }
        public DbSet<UserTa> UserTas { get; set; }
    }
}
