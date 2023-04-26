using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Models.Queries;

namespace OnlineUniversityWebAPI.Application.Services.Interfaces
{
    public interface IStudentService
    { 
        void Delete(int id);
        PagedResult<StudentDto> GetAll(StudentQuery studentQuery);
        StudentDto GetById(int id);
        void Update(int id, UpdateStudentDto dto);
    }
}
