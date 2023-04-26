using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineUniversityWebAPI.Application.Exceptions;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Domain.Entities;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Models.Queries;
using System.Linq.Expressions;

namespace OnlineUniversityWebAPI.Services
{

    public class StudentService : IStudentService
    {
        private readonly OnlineUniversityWebAPIDbContext _dbContext;
        private readonly IMapper _mapper;

        public StudentService(OnlineUniversityWebAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public StudentDto GetById(int id)
        {
            var student = _dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Grades)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
                throw new NotFoundException("Student not found");

            var result = _mapper.Map<StudentDto>(student);
            return result;
        }

        public PagedResult<StudentDto> GetAll(StudentQuery query)
        {
            var baseQuery = _dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Grades)
                .Where(s => query.SearchPhrase == null || s.FirstName.ToLower().Contains(query.SearchPhrase.ToLower())
                                                       || s.LastName.ToLower().Contains(query.SearchPhrase.ToLower()));

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Student, object>>>
                {
                    { nameof(Student.FirstName), s => s.FirstName },
                    { nameof(Student.LastName), s => s.LastName },
                    { nameof(Student.Age), s => s.Age },
                    { nameof(Student.Id), s => s.Id },
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                        baseQuery.OrderBy(selectedColumn) :
                        baseQuery.OrderByDescending(selectedColumn);
            }

            var students = baseQuery
                .Skip(query.PageSize * (query.PageNumber -1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            var result = new PagedResult<StudentDto>(studentDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public void Delete(int id)
        {
            
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (student == null) 
                throw new NotFoundException("Student not found");

            _dbContext.Remove(student);
            _dbContext.SaveChanges();
        
        }

        public void Update(int id, UpdateStudentDto dto)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                throw new NotFoundException("Student not found");

            student.FirstName = dto.FirstName;

            _dbContext.SaveChanges();
        }
    }
}
