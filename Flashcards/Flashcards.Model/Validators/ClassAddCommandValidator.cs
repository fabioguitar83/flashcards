using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class ClassAddCommandValidator : AbstractValidator<ClassAddCommand>
    {
        public ClassAddCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.IdUser)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
        }
 
    }
}
