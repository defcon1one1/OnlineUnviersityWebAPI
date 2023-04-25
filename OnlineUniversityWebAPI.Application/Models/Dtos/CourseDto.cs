using OnlineUniversityWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class CourseDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public List<ModuleDto> Modules { get; set; }
        //public List<Enrollment> Enrollments { get; set; }

    }
}
