using Catalog.Domain.Entities;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name).NotNull().NotEmpty().WithMessage("Category name is required");
            RuleFor(category => category.Name).Length(1, 50).WithMessage("Category name length can be maximum of 50 characters");
        }
    }
}

