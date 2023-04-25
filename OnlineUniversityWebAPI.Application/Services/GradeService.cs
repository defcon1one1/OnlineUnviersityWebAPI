using AutoMapper;
using OnlineUniversityWebAPI.Application.Exceptions;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using OnlineUniversityWebAPI.Application.Models.Dtos;

namespace OnlineUniversityWebAPI.Services
{
    public class GradeService : IGradeService
    {

        private readonly OnlineUniversityWebAPIDbContext _dbContext;
        private readonly IMapper _mapper;

        public GradeService(OnlineUniversityWebAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Update(int studentId, int enrollmentId, int gradeId, UpdateGradeDto dto)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new NotFoundException($"Student not found");

            var enrollment = _dbContext.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null || enrollment.StudentId != studentId)
                throw new NotFoundException($"Enrollment not found");

            var grade = _dbContext.Grades.FirstOrDefault(g => g.Id == gradeId);
            if (grade == null || enrollment.StudentId != studentId || grade.EnrollmentId != enrollmentId)
                throw new NotFoundException($"Grade not found");

            grade.Value = dto.Value;
            _dbContext.SaveChanges();

        }



        public List<GradeDto> GetAll(int studentId, int enrollmentId)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new NotFoundException($"Student not found");

            var enrollment = _dbContext.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null || enrollment.StudentId != studentId)
                throw new NotFoundException($"Enrollment not found");

            var grades = _dbContext.Grades.Where(e => e.EnrollmentId == enrollmentId);
            var gradesDto = _mapper.Map<List<GradeDto>>(grades);

            return gradesDto;

        }

        public GradeDto GetById(int studentId, int enrollmentId, int gradeId)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new NotFoundException($"Student not found");

            var enrollment = _dbContext.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null || enrollment.StudentId != studentId)
                throw new NotFoundException($"Enrollment not found");

            var grade = _dbContext.Grades.FirstOrDefault(g => g.Id == gradeId);
            if (grade == null || enrollment.StudentId != studentId || grade.EnrollmentId != enrollmentId)
                throw new NotFoundException($"Grade not found");

            var gradeDto = _mapper.Map<GradeDto>(grade);

            return gradeDto;

        }
    }
}
