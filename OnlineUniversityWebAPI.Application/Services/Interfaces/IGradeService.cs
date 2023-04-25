using OnlineUniversityWebAPI.Application.Models.Dtos;

namespace OnlineUniversityWebAPI.Application.Services.Interfaces
{
    public interface IGradeService
    {
        void Update(int studentId, int enrollmentId, int gradeId, UpdateGradeDto dto);

        GradeDto GetById(int studentId, int enrollmentId, int gradeId);
        List<GradeDto> GetAll(int studentId, int enrollmentId);
    }
}
