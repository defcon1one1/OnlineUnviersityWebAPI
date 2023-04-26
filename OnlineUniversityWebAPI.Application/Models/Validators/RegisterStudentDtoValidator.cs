using FluentValidation;
using OnlineUniversityWebAPI.Application.Models.Dtos;
using OnlineUniversityWebAPI.Infrastructure.Persistence;

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
            .Must(BeAValidDate)
            .WithMessage("Date of birth must be in yyyy-mm-dd format");

        RuleFor(x => x)
            .Must(x => BeAtLeast18YearsOld(x.DateOfBirth))
            .WithMessage("Student must be at least 18 years old");

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

    private bool BeAValidDate(DateTime date)
    {
        return date != default && date.Year > 1900;
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.UtcNow.AddYears(-18);
    }
}
