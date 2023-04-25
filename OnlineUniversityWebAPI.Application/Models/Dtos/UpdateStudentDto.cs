using System.ComponentModel.DataAnnotations;

namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class UpdateStudentDto
    {
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
