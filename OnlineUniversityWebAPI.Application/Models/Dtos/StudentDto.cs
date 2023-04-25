namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EnrollmentDto> Enrollments { get; set; }
        public string Email { get; set; }
    }
}
