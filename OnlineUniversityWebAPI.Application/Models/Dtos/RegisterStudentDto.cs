using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineUniversityWebAPI.Application.Models.Dtos
{
    public class RegisterStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }   
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; } = 2;
    }
}
