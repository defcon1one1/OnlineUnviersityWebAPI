using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Models.Queries;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Domain.Entities;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly OnlineUniversityWebAPIDbContext _dbContext;
        private readonly IMapper _mapper;

        public CourseService(OnlineUniversityWebAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PagedResult<CourseDto> GetAll(CourseQuery query)
        {
            var baseQuery = _dbContext.Courses
                .Include(c => c.Modules)
                .Where(c => query.SearchPhrase == null || c.Name.ToLower().Contains(query.SearchPhrase.ToLower()));


            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Course, object>>>
                {
                    { nameof(Course.Name), s => s.Name },
                    { nameof(Course.Id), s => s.Id },
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                        baseQuery.OrderBy(selectedColumn) :
                        baseQuery.OrderByDescending(selectedColumn);
            }

            var courses = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var courseDtos = _mapper.Map<List<CourseDto>>(courses);

            var result = new PagedResult<CourseDto>(courseDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public CourseDto GetById(int id)
        {
            var course = _dbContext.Courses
                .Include(c => c.Modules)
                .Where(c => c.Id == id)
                .FirstOrDefault();
            var courseDto = _mapper.Map<CourseDto>(course);
            return courseDto;
        }
    }
}
