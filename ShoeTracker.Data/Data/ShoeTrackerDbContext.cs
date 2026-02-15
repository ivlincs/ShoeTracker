namespace ShoeTracker.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using ShoeTracker.Data.Models.Entities;

    public class ShoeTrackerDbContext : IdentityDbContext
    {
        public ShoeTrackerDbContext(DbContextOptions<ShoeTrackerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Shoe> Shoes { get; set; } = null!;
        public DbSet<Run> Runs { get; set; } = null!;
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for the relationships
            modelBuilder.Entity<Shoe>()
                .HasMany(s => s.Runs)
                .WithOne(r => r.Shoe)
                .HasForeignKey(r => r.ShoeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
