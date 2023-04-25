using System.ComponentModel.DataAnnotations;

namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class UpdateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
