using Microsoft.EntityFrameworkCore;
using OnlineUniversityWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Infrastructure.Persistence
{
    public class OnlineUniversityWebAPIDbContext : DbContext
    {


        public OnlineUniversityWebAPIDbContext(DbContextOptions<OnlineUniversityWebAPIDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnlineUniversityWebAPIDb;Trusted_Connection=true;");
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>(eb =>
            {
                eb.HasKey(e => e.Id);

                eb.HasIndex(e => new { e.CourseId, e.StudentId }).IsUnique();

                eb.HasOne(e => e.Student)
                    .WithMany(s => s.Enrollments)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                eb.HasMany(ce => ce.Grades)
                    .WithOne(mg => mg.Enrollment)
                    .HasForeignKey(mg => mg.EnrollmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Course>(eb =>
            {

                eb.HasKey(e => e.Id);

                eb.Property(e => e.IsActive).HasDefaultValue(true);

                eb.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                eb.HasMany(c => c.Modules)
                    .WithOne(m => m.Course)
                    .HasForeignKey(m => m.CourseId);
            });

            modelBuilder.Entity<User>(eb =>
            {
                eb.HasKey(e => e.Id);

                eb.Property(e => e.Name)
                    .HasMaxLength(35)
                    .IsRequired();

                eb.HasOne(r => r.Role)
                  .WithMany()
                  .HasForeignKey(r => r.RoleId);

            });


            modelBuilder.Entity<Student>(eb =>
            {
                eb.HasMany(s => s.Enrollments)
                    .WithOne(ce => ce.Student)
                    .HasForeignKey(ce => ce.StudentId);
            });

            modelBuilder.Entity<Grade>(eb =>
            {
                eb.HasKey(e => e.Id);

                eb.HasCheckConstraint("CK_Grade_Value", "[Value] BETWEEN 3 AND 5");

                eb.HasIndex(e => new { e.ModuleId, e.EnrollmentId }).IsUnique();

                eb.HasOne(mg => mg.Module)
                    .WithMany()
                    .HasForeignKey(mg => mg.ModuleId)
                    .OnDelete(DeleteBehavior.NoAction);

                eb.HasOne(g => g.Enrollment)
                    .WithMany(ce => ce.Grades)
                    .HasForeignKey(mg => mg.EnrollmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


        }
    }
}
