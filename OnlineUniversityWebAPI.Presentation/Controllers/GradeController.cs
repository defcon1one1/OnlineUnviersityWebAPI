using Microsoft.AspNetCore.Mvc;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Services;

namespace OnlineUniversityWebAPI.Presentation.Controllers
{
    [Route("api/student/{studentId}/enrollment/{enrollmentId}/grade/")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public ActionResult<List<GradeDto>> GetAll([FromRoute] int studentId, [FromRoute] int enrollmentId)
        {
            var moduleGradeDtos = _gradeService.GetAll(studentId, enrollmentId);
            return Ok(moduleGradeDtos);
        }

        [HttpGet("{gradeId}")]
        public ActionResult<GradeDto> GetById([FromRoute] int studentId, [FromRoute] int enrollmentId, [FromRoute] int gradeId)
        {
            var gradeDto = _gradeService.GetById(studentId,enrollmentId, gradeId);
            return Ok(gradeDto);
        }

        [HttpPut("{gradeId}")]
        public ActionResult Update([FromRoute] int studentId, [FromRoute] int enrollmentId, [FromRoute] int gradeId, [FromBody] UpdateGradeDto dto) 
        {
            _gradeService.Update(studentId, enrollmentId, gradeId, dto);
            return Ok();
        }

    }
}
