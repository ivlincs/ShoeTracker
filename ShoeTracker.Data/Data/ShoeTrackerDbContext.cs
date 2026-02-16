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
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for the relationships
            modelBuilder.Entity<Shoe>()
                .HasMany(s => s.Runs)
                .WithOne(r => r.Shoe)
                .HasForeignKey(r => r.ShoeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Shoes)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed for categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Road running" },
                     new Category { Id = 2, Name = "Trail running"},
                     new Category { Id = 3, Name = "Race running" },
                     new Category { Id = 4, Name = "Daily training"}
            );
        }
    }
}
