using OnlineUniversityWebAPI.Application.Models.Dtos;

namespace OnlineUniversityWebAPI.Application.Services.Interfaces
{
    public interface IEnrollmentService
    {
        int Create(int studentId, CreateEnrollmentDto dto);
        List<EnrollmentDto> GetAll(int studentId);
        EnrollmentDto GetById(int studentId, int enrollmentId);
        void Delete(int studentId, int enrollmentId);
    }


}
