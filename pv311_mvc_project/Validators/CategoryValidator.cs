using FluentValidation;
using pv311_mvc_project.Models;

namespace pv311_mvc_project.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Поле обов'язкове")
                .MaximumLength(100).WithMessage("Максимальна довжина 100 символів");
        }
    }
}
