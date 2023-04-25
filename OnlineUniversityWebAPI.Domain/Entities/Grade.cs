using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int? Value { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }


    }
}
