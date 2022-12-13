using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class FlashcardAddCommandValidator : AbstractValidator<FlashcardAddCommand>
    {
        public FlashcardAddCommandValidator()
        {

            RuleFor(x => x.IdLesson)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Front)                  
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Back) 
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
