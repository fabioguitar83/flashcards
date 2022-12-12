using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class ClassListCommandValidator : AbstractValidator<ClassListCommand>
    {
        public ClassListCommandValidator()
        {
            RuleFor(x => x.IdUser)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
        }
 
    }
}
