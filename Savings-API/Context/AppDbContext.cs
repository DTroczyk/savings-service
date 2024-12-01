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
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Saving>()
                .HasOne(s => s.User)
                .WithMany(u => u.Savings)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Owner)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
