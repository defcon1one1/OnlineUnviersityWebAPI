using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Domain.Entities
{
    public class Student : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public int Age { get => DateTime.Now.Year - DateOfBirth.Year; }

    }
}
