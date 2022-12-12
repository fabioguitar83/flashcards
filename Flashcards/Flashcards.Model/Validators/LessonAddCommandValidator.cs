using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class LessonAddCommandValidator: AbstractValidator<LessonAddCommand> 
    {
        public LessonAddCommandValidator()
        {
            RuleFor(x => x.IdClass)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre 2 e 200 caracteres");

        }
    }
}
