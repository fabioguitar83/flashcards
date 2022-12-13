using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class FlashcardListCommandValidator: AbstractValidator<FlashcardListCommand>
    {
        public FlashcardListCommandValidator()
        {
            RuleFor(x => x.IdLesson)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
