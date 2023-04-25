using OnlineUniversityWebAPI.Domain.Entities;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Infrastructure.Seeders
{
    public class Seeder
    {
        private readonly OnlineUniversityWebAPIDbContext _dbContext;
        public Seeder(OnlineUniversityWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Seed()
        {
            // check connection
            if (await _dbContext.Database.CanConnectAsync())
            {
                // check if tables are empty
                if (!_dbContext.Students.Any() && !_dbContext.Enrollments.Any() && !_dbContext.Courses.Any() && !_dbContext.Modules.Any() && !_dbContext.Grades.Any())
                {


                    //roles -------------------------------------------------------------
                    var roles = new List<Role>()
                    {
                        new Role() { Name = "Admin" },
                        new Role() { Name = "Student" }
                    };

                    foreach (var role in roles)
                    {
                        _dbContext.Add(role);
                    }
                    await _dbContext.SaveChangesAsync();

                    // students -------------------------------------------------------------
                    var students = new List<Student>
            {
                new Student { FirstName = "John", LastName = "Smith", DateOfBirth = new DateTime(1992, 12, 29), Email = "johnsmith@mail.com", RoleId = 2, PasswordHash = HashPassword("johnsmith@mail.com") },
                new Student { FirstName = "Albert", LastName = "Adams", DateOfBirth = new DateTime(1982, 12, 29), Email = "albertadams@mail.com", RoleId = 2, PasswordHash = HashPassword("albertadams@mail.com") },
                new Student { FirstName = "Amber", LastName = "Hebert", DateOfBirth = new DateTime(1992, 12, 29), Email = "amberhebert@mail.com", RoleId = 2, PasswordHash = HashPassword("amberhebert@mail.com") },
                new Student { FirstName = "Aliyah", LastName = "Holmes", DateOfBirth = new DateTime(1992, 12, 29), Email = "aliyahholmes@mail.com", RoleId = 2, PasswordHash = HashPassword("aliyahholmes@mail.com") },
                new Student { FirstName = "Danielle", LastName = "Goodman", DateOfBirth = new DateTime(1992, 12, 29), Email = "daniellegoodman@mail.com", RoleId = 2, PasswordHash = HashPassword("daniellegoodman@mail.com") },
                new Student { FirstName = "Travis", LastName = "Riley", DateOfBirth = new DateTime(1992, 12, 29), Email = "travisriley@mail.com", RoleId = 2, PasswordHash = HashPassword("travisriley@mail.com") },
                new Student { FirstName = "Frazer", LastName = "Mason", DateOfBirth = new DateTime(1992, 12, 29), Email = "frazermason@mail.com", RoleId = 2, PasswordHash = HashPassword("frazermason@mail.com") },
                new Student { FirstName = "Cory", LastName = "Thompson", DateOfBirth = new DateTime(1992, 12, 29), Email = "corythompson@mail.com", RoleId = 2, PasswordHash = HashPassword("corythompson@mail.com") },
                new Student { FirstName = "Sid", LastName = "Hodges", DateOfBirth = new DateTime(1992, 12, 29), Email = "sidhodges@mail.com", RoleId = 2, PasswordHash = HashPassword("sidhodges@mail.com") },
                new Student { FirstName = "Travis", LastName = "Barlow", DateOfBirth = new DateTime(1992, 12, 29), Email = "travisbarlow@mail.com", RoleId = 2, PasswordHash = HashPassword("travisbarlow@mail.com") },
                new Student { FirstName = "Jonas", LastName = "Turner", DateOfBirth = new DateTime(1992, 12, 29), Email = "jonasturner@mail.com", RoleId = 2, PasswordHash = HashPassword("jonasturner@mail.com") },
                new Student { FirstName = "Hamzah", LastName = "Barrera", DateOfBirth = new DateTime(1992, 12, 29), Email = "hamzahbarrera@mail.com", RoleId = 2, PasswordHash = HashPassword("hamzahbarrera@mail.com") },
                new Student { FirstName = "Derek", LastName = "Dean", DateOfBirth = new DateTime(1992, 12, 29), Email = "derekdean@mail.com", RoleId = 2, PasswordHash = HashPassword("derekdean@mail.com") },
                new Student { FirstName = "Alexis", LastName = "Lynn", DateOfBirth = new DateTime(1992, 12, 29), Email = "alexislynn@mail.com", RoleId = 2, PasswordHash = HashPassword("alexislynn@mail.com") },
                new Student { FirstName = "Joel", LastName = "Cohen", DateOfBirth = new DateTime(1992, 12, 29), Email = "joelcohen@mail.com", RoleId = 2, PasswordHash = HashPassword("joelcohen@mail.com") },
                new Student { FirstName = "Sam", LastName = "Guzman", DateOfBirth = new DateTime(1992, 12, 29), Email = "samguzman@mail.com", RoleId = 2, PasswordHash = HashPassword("samguzman@mail.com") },

            };


                    foreach (var student in students)
                    {
                        _dbContext.Add(student);
                    }

                    // save changes
                    await _dbContext.SaveChangesAsync();

                    // ---------------------courses 
                    var courses = new List<Course>
            {
                new Course { Name = "Math" },
                new Course { Name = "History" },
                new Course { Name = "Biology"},
                new Course { Name = "Chemistry" },
                new Course { Name = "Philosophy" },
                new Course { Name = "Economics" },
                new Course { Name = "Finances" },
                new Course { Name = "Business" },

            };

                    foreach (var course in courses)
                    {
                        _dbContext.Add(course);
                    }

                    //save changes
                    await _dbContext.SaveChangesAsync();



                    // ---------------------modules
                    var modules = new List<Module>
            {
                new Module { Name = "Algebra", CourseId = 1 },
                new Module { Name = "Calculus", CourseId = 1 },
                new Module { Name = "Statistics", CourseId = 1 },
                new Module { Name = "Theory of probability", CourseId = 1 },

                new Module { Name = "Medieval history", CourseId = 2 },
                new Module { Name = "Modern history", CourseId = 2 },
                new Module { Name = "Ancient history", CourseId = 2},

                new Module { Name = "Science of evolution", CourseId = 3 },
                new Module { Name = "History of biology", CourseId = 3 },
                new Module { Name = "Genetics", CourseId = 3},

                new Module { Name = "Basics of chemistry", CourseId = 4 },
                new Module { Name = "Organic chemistry", CourseId = 4 },
                new Module { Name = "Practical chemistry", CourseId = 4},
                
                new Module { Name = "Modernism", CourseId = 5},
                new Module { Name = "Platonism", CourseId = 5},

                new Module { Name = "Microeconomics", CourseId = 6},
                new Module { Name = "Macroeconomics", CourseId = 6},

                new Module { Name = "Tax law", CourseId = 7},
                new Module { Name = "Property law", CourseId = 7},

                new Module { Name = "Management", CourseId = 8},


            };

                    foreach (var module in modules)
                    {
                        _dbContext.Add(module);
                    }

                    // save changes
                    await _dbContext.SaveChangesAsync();


                    // ---------------------enrollments
                    var enrollments = new List<Enrollment>
            {
                new Enrollment {IsCompleted = false, StudentId = 1, CourseId = 1 },
                new Enrollment {IsCompleted = true, StudentId = 1, CourseId = 2 },

                new Enrollment {IsCompleted = false, StudentId = 2, CourseId = 2 },
                new Enrollment {IsCompleted = false, StudentId = 2, CourseId = 3 },

                new Enrollment {IsCompleted = false, StudentId = 3, CourseId = 4 }
            };

                    foreach (var enrollment in enrollments)
                    {
                        _dbContext.Add(enrollment);
                    }

                    // save changes
                    await _dbContext.SaveChangesAsync();


                    // ---------------------grades
                    var grades = new List<Grade>
            {
                new Grade { Value = 3, ModuleId = 1, EnrollmentId = 1 },
                new Grade { Value = 5, ModuleId = 2, EnrollmentId = 1 },
                new Grade { Value = null, ModuleId = 3, EnrollmentId = 1 },
                new Grade { Value = 4, ModuleId = 4, EnrollmentId = 1 },

                new Grade { Value = 3, ModuleId = 5, EnrollmentId = 2 },
                new Grade { Value = 5, ModuleId = 6, EnrollmentId = 2 },
                new Grade { Value = 4, ModuleId = 7, EnrollmentId = 2 },

                new Grade { Value = 3, ModuleId = 5, EnrollmentId = 3 },
                new Grade { Value = 5, ModuleId = 6, EnrollmentId = 3 },
                new Grade { Value = null, ModuleId = 7, EnrollmentId = 3 },

                new Grade { Value = 4, ModuleId = 8, EnrollmentId = 4 },
                new Grade { Value = 4, ModuleId = 9, EnrollmentId = 4 },
                new Grade { Value = null, ModuleId = 10, EnrollmentId = 4 },

                new Grade { Value = null, ModuleId = 11, EnrollmentId = 5 },
                new Grade { Value = null, ModuleId = 12, EnrollmentId = 5 },
                new Grade { Value = null, ModuleId = 13, EnrollmentId = 5 }

            };


                    foreach (var grade in grades)
                    {
                        _dbContext.Add(grade);
                    }

                    // save changes
                    await _dbContext.SaveChangesAsync();


                }
            }




        }

        private string HashPassword(string password)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {

                byte[] salt;
                byte[] buffer2;

                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
                {
                    salt = bytes.Salt;
                    buffer2 = bytes.GetBytes(0x20);
                }
                byte[] dst = new byte[0x31];
                Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
                Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
                Convert.ToBase64String(dst);
                var hash = Convert.ToBase64String(dst);
                return hash;
            }
        }
    }
}
