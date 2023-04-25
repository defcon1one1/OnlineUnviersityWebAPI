using FluentValidation;
using OnlineUniversityWebAPI.Application.Models.Queries;
using OnlineUniversityWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineUniversityWebAPI.Application.Models.Validators
{
    public class QueryValidator : AbstractValidator<Query>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 25 };
        private string[] allowedSortByColumnNames = new[] { nameof(Student.Name), nameof(Student.Id) };
        public QueryValidator()
        {
            RuleFor(q => q.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(q => q.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(", ", allowedPageSizes)}]");
                }
            });
            RuleFor(q => q.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"SortBy must be empty or in [{string.Join(", ", allowedSortByColumnNames)}]");
        }
    }
}
