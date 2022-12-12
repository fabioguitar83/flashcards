using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class ClassRemoveCommandValidator : AbstractValidator<ClassRemoveCommand>
    {
        public ClassRemoveCommandValidator()
        {
            RuleFor(x => x.IdUser)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
        }
 
    }
}
