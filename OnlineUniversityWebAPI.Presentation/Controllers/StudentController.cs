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
    [Route("api/student")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) 
        {
            _studentService = studentService;
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDto> Get([FromRoute]int id)
        {
            var studentDto = _studentService.GetById(id);
            return Ok(studentDto);
        }
        [HttpGet("all")]
        
        public ActionResult<IEnumerable<StudentDto>> GetAll([FromQuery] StudentQuery query)
        {
            var studentDtos = _studentService.GetAll(query);
            return Ok(studentDtos);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _studentService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody]UpdateStudentDto dto, [FromRoute] int id)
        {
            _studentService.Update(id, dto);
            return Ok();
        }

        
    }
}
