using BlazorFeedbackApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFeedbackApp.DAL.Contexts
{
    public class FeedbackAppDbContext : DbContext
    {
        public FeedbackAppDbContext(DbContextOptions<FeedbackAppDbContext> options) : base(options)
        {
        }

        public DbSet<Feedback> FeedbackEntries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.ObjectId)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}