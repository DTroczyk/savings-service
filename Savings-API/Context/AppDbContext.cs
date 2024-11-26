using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Savings_API.Context
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Saving> Savings { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Saving>()
                .HasOne(s => s.Goal)
                .WithMany(g => g.Savings)
                .HasForeignKey(s => s.GoalId)
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<Saving>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
