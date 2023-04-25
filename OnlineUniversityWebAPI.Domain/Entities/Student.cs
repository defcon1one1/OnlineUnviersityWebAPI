using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Domain.Entities
{
    public class Student : User
    {
        public List<Enrollment> Enrollments { get; set; }

    }
}
