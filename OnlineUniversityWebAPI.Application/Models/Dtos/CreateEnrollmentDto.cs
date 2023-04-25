using OnlineUniversityWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class CreateEnrollmentDto
    {
        public bool IsCompleted { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public List<Module> Modules { get; set; } = new List<Module>();
        public List<Grade>? Grades { get; set; } = new List<Grade>();
    }
}
