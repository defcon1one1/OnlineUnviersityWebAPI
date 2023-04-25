namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int? Value { get; set; }
        public int ModuleId { get; set; }
        public int EnrollmentId { get; set; }
    }
}
