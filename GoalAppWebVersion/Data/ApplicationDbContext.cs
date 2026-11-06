using Microsoft.EntityFrameworkCore;
using GoalAppWebVersion.Models.Entities;

namespace GoalAppWebVersion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Goal> Goal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HabitMakeGoal>();
            modelBuilder.Entity<HabitBreakGoal>();
            modelBuilder.Entity<ExerciseGoal>();
        }
    }
}
