using FluentValidation;

namespace HomeExpenses.Application.DTOs.Person;

public sealed record CreatePersonDto(string Name, int Age)
{
    public void ValidateAndThrow() => new Validator().ValidateAndThrow(this);

    private sealed class Validator : AbstractValidator<CreatePersonDto>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O Nome é obrigatório.")
                .MaximumLength(200)
                .WithMessage("O Nome deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Idade deve ser maior ou igual a 0.");
        }
    }
}