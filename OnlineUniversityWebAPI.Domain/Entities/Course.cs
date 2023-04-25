using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public List<Module> Modules { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public int ModuleCount { get => Modules.Count; }

    }
}
