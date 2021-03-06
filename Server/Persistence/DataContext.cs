using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Persistence {
    public class DataContext : DbContext, IPersistenceContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        // Example:
        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }
        public DbSet<UserAssignment> UserAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Course>()
                .HasMany(x => x.Assignments);
        }
    }
}