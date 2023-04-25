using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Services;

namespace OnlineUniversityWebAPI.Presentation.Controllers
{
    [Route("api/student/{studentId}/enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int studentId, [FromBody] CreateEnrollmentDto dto)
        {
            var newEnrollmentId = _enrollmentService.Create(studentId, dto);
            
            return Created($"api/student/{studentId}/enrollment/{newEnrollmentId}", null);
        }

        [HttpGet("{enrollmentId}")]
        public ActionResult<EnrollmentDto> Get([FromRoute] int studentId, [FromRoute] int enrollmentId) 
        {
            EnrollmentDto enrollment = _enrollmentService.GetById(studentId, enrollmentId);
            return Ok(enrollment);
        }

        [HttpGet]
        public ActionResult<List<EnrollmentDto>> GetAll([FromRoute] int studentId) 
        {
            var enrollmentDtos = _enrollmentService.GetAll(studentId);
            return Ok(enrollmentDtos);
        }

        [HttpDelete("{enrollmentId}")]
        public ActionResult Delete([FromRoute] int studentId, [FromRoute] int enrollmentId) 
        { 
            _enrollmentService.Delete(studentId, enrollmentId);
            return NoContent();
        }

    }
}
