using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineUniversityWebAPI.Application.Exceptions;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Domain.Entities;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using OnlineUniversityWebAPI.Application.Models.Dtos;

namespace OnlineUniversityWebAPI.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly OnlineUniversityWebAPIDbContext _dbContext;
        private readonly IMapper _mapper;
        public EnrollmentService(OnlineUniversityWebAPIDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int Create(int studentId, CreateEnrollmentDto dto)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);

            if (student == null)
                throw new NotFoundException("Student not found");

            var enrollmentEntity = _mapper.Map<Enrollment>(dto);

            var course = _dbContext.Courses
                .Include(c => c.Modules)
                .FirstOrDefault(c => c.Id == enrollmentEntity.CourseId);

            foreach (var module in course.Modules)
            {
                enrollmentEntity.Grades.Add(new Grade() { EnrollmentId = enrollmentEntity.Id, ModuleId = module.Id, Value = null });
            }

            enrollmentEntity.StudentId = student.Id;

            _dbContext.Enrollments.Add(enrollmentEntity);
            _dbContext.SaveChanges();

            return enrollmentEntity.Id;
        }

        public EnrollmentDto GetById(int studentId, int enrollmentId)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new NotFoundException($"Student not found");

            var enrollment = _dbContext.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null || enrollment.StudentId != studentId)
                throw new NotFoundException($"Enrollment not found");

            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollment);
            return enrollmentDto;
        }


        public List<EnrollmentDto> GetAll(int studentId)
        {
            var student = _dbContext.Students
                .Include(s => s.Enrollments)
                .FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new NotFoundException($"Student not found");

            var enrollmentDtos = _mapper.Map<List<EnrollmentDto>>(student.Enrollments);
            return enrollmentDtos;
        }

        public void Delete(int studentId, int enrollmentId)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new NotFoundException($"Student not found");

            var enrollment = _dbContext.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null || enrollment.StudentId != studentId)
                throw new NotFoundException($"Enrollment not found");

            _dbContext.Remove(enrollment);
            _dbContext.SaveChanges();
        }

    }


}
