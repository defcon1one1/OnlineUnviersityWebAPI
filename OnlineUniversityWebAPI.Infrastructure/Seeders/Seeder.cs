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
                new Student { Name = "John Smith", Email = "johnsmith@mail.com", RoleId = 2, PasswordHash = HashPassword("johnsmith@mail.com") },
                new Student { Name = "Anna Doe", Email = "annadoe@mail.com", RoleId = 2, PasswordHash = HashPassword("annadoe@mail.com")  },
                new Student { Name = "Bob Johnson", Email = "bobjohnson@mail.com", RoleId = 2, PasswordHash = HashPassword("bobjohnson@mail.com")  },
                new Student { Name = "Frank Underwood", Email = "frankunderwood@mail.com", RoleId = 2, PasswordHash = HashPassword("frankunderwood@mail.com") },
                new Student { Name = "Matthew Harris", Email = "matthewharris@mail.com", RoleId = 2, PasswordHash = HashPassword("matthewharris@mail.com")  },
                new Student { Name = "Gregory Taylor", Email = "gregorytaylor@mail.com", RoleId = 2, PasswordHash = HashPassword("gregorytaylor@mail.com")  }

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
                new Course { Name = "Chemistry" }
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

                new Module { Name = "Medieval history", CourseId = 2 },
                new Module { Name = "Modern history", CourseId = 2 },
                new Module { Name = "Ancient history", CourseId = 2},

                new Module { Name = "Science of evolution", CourseId = 3 },
                new Module { Name = "History of biology", CourseId = 3 },
                new Module { Name = "Genetics", CourseId = 3},

                new Module { Name = "Basics of chemistry", CourseId = 4 },
                new Module { Name = "Organic chemistry", CourseId = 4 },
                new Module { Name = "Practical chemistry", CourseId = 4}


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

                new Grade { Value = 3, ModuleId = 4, EnrollmentId = 2 },
                new Grade { Value = 5, ModuleId = 5, EnrollmentId = 2 },
                new Grade { Value = 4, ModuleId = 6, EnrollmentId = 2 },

                new Grade { Value = 3, ModuleId = 4, EnrollmentId = 3 },
                new Grade { Value = 5, ModuleId = 5, EnrollmentId = 3 },
                new Grade { Value = null, ModuleId = 6, EnrollmentId = 3 },

                new Grade { Value = 4, ModuleId = 7, EnrollmentId = 4 },
                new Grade { Value = 4, ModuleId = 8, EnrollmentId = 4 },
                new Grade { Value = null, ModuleId = 9, EnrollmentId = 4 },

                new Grade { Value = null, ModuleId = 10, EnrollmentId = 5 },
                new Grade { Value = null, ModuleId = 11, EnrollmentId = 5 },
                new Grade { Value = null, ModuleId = 12, EnrollmentId = 5 }

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
