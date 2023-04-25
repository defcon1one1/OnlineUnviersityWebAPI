namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<GradeDto>? Grades { get; set; }
    }
}