using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class FlashcardUpdateCommandValidator: AbstractValidator<FlashcardUpdateCommand>
    {
        public FlashcardUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Front)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Back)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
