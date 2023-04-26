using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Models.Queries;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Services;

namespace OnlineUniversityWebAPI.Presentation.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("all")]
        public ActionResult<PagedResult<CourseDto>> GetAll([FromQuery] CourseQuery query)
        {
            var result = _courseService.GetAll(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDto> GetById(int id)
        {
            var courseDto = _courseService.GetById(id);
            return Ok(courseDto);
        }

    }
}
