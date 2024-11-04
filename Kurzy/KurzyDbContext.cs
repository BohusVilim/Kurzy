using Kurzy.Models;
using Microsoft.EntityFrameworkCore;

namespace Kurzy
{
    public class KurzyDbContext : DbContext
    {
        public KurzyDbContext(DbContextOptions<KurzyDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CoursesStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<CourseStudent>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.CoursesStudents)
                .HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<CourseStudent>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.CoursesStudents)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
