using Catalog.Domain.Entities;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(item => item.Name).NotNull().NotEmpty().WithMessage("Item name is required");
            RuleFor(item => item.Name).Length(1, 50).WithMessage("Item name length can be maximum of 50 characters");
            RuleFor(item => item.Category).NotNull().WithMessage("Item category is required");
            RuleFor(item => item.Amount).GreaterThan(0).WithMessage("Item amount should be a positive int");
        }
    }
}

