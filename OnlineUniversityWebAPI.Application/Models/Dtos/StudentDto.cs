using OnlineUniversityWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public int Age => CalculateAge();

        private int CalculateAge()
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
