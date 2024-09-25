using expense_tracker.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for the Categories table
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Groceries" },
                new Category { Id = 2, Name = "Leisure" },
                new Category { Id = 3, Name = "Electronics" },
                new Category { Id = 4, Name = "Utilities" },
                new Category { Id = 5, Name = "Clothing" },
                new Category { Id = 6, Name = "Health" },
                new Category { Id = 7, Name = "Others" }
            );
        }
    }
}
