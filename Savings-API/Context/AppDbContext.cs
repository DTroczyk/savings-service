using Microsoft.EntityFrameworkCore;

namespace Savings_API.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Saving> Savings { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

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
                .OnDelete(DeleteBehavior.SetNull);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
