using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class LessonListCommandValidator: AbstractValidator<LessonListCommand>
    {
        public LessonListCommandValidator()
        {
            RuleFor(x => x.IdClass)
               .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
