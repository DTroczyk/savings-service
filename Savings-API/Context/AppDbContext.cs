using Microsoft.EntityFrameworkCore;

namespace Savings_API.Context
{
    public partial class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Saving> Savings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Saving>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InsertDate).HasColumnType("datetime");
                entity.Property(e => e.Date).HasColumnType("date");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
