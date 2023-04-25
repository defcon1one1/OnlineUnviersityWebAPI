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
                .NotEmpty()
                .Must(dob => dob <= DateTime.Now.AddYears(-18))
                .WithMessage("You must be at least 18 years old");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .Must(dob => DateTime.TryParse(dob.ToString(), out _))
                .WithMessage("Date of birth must be a valid date");

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

    }
}
