using FluentValidation;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.DTOs.Category;

public record CreateCategoryDto(string Description, CategoryPurposeEnum Purpose)
{
    public void ValidateAndThrow() => new Validator().ValidateAndThrow(this);

    public sealed class Validator : AbstractValidator<CreateCategoryDto>
    {
        public Validator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("A descrição é obrigatória.")
                .MaximumLength(400)
                .WithMessage("A descrição deve conter no máximo 400 caracteres.");

            RuleFor(x => x.Purpose)
                .IsInEnum()
                .WithMessage("Propósito Inválido.");
        }
}
}
