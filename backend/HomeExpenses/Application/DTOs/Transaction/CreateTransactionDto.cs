using FluentValidation;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.DTOs.Transaction;

public sealed record CreateTransactionDto(
    string Description,
    decimal Amount,
    TransactionKindEnum Kind,
    Guid CategoryId,
    Guid PersonId)
{
    public void ValidateAndThrow() => new Validator().ValidateAndThrow(this);

    private sealed class Validator : AbstractValidator<CreateTransactionDto>
    {
        public Validator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("A descriÃ§Ã£o Ã© obrigatÃ³ria.")
                .MaximumLength(400)
                .WithMessage("A descriÃ§Ã£o deve conter no mÃ¡ximo 400 caracteres.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("O valor da transaÃ§Ã£o deve ser maior que zero.");

            RuleFor(x => x.Kind)
                .IsInEnum()
                .WithMessage("Tipo de transaÃ§Ã£o invÃ¡lido.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("A categoria Ã© obrigatÃ³ria.");

            RuleFor(x => x.PersonId)
                .NotEmpty()
                .WithMessage("A pessoa Ã© obrigatÃ³ria.");
        }
    }
}
