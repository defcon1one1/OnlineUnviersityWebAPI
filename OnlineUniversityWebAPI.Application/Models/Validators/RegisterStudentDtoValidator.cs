using FluentValidation;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using System;

namespace OnlineUniversityWebAPI.Application.Models.Validators
{
    public class RegisterStudentDtoValidator : AbstractValidator<RegisterStudentDto>
    {
        public RegisterStudentDtoValidator(OnlineUniversityWebAPIDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords are not the same");

            RuleFor(x => x.DateOfBirth)
                .Must(date => DateTime.TryParse(date.ToString(), out _))
                .WithMessage("Invalid date format");



            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is already registered");
                    }
                });
        }

        private bool BeAValidDate(string dateOfBirthString)
        {
            DateTime dateOfBirth;
            return DateTime.TryParse(dateOfBirthString, out dateOfBirth);
        }

        private bool BeAtLeast18YearsAgo(DateTime dateOfBirth)
        {
            return dateOfBirth.AddYears(18) <= DateTime.Now;
        }
    }
}
