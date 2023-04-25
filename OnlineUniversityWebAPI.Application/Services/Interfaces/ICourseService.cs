using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Application.Services.Interfaces
{
    public interface ICourseService
    {
        CourseDto GetById(int id);
        PagedResult<CourseDto> GetAll(Query query);
    }
}
