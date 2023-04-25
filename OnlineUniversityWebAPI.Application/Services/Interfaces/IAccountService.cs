using OnlineUniversityWebAPI.Application.Models.Dtos;

namespace OnlineUniversityWebAPI.Application.Services.Interfaces
{
    public interface IAccountService
    {
        void RegisterStudent(RegisterStudentDto dto);
        string GenerateJwt(LoginDto dto);
    }
}
