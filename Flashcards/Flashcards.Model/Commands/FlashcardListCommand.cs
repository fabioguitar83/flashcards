using Flashcards.Domain.Responses;
using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class FlashcardListCommand : CommandBase, IRequest<IEnumerable<FlashcardListResponse>>
    {
        public int IdLesson { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new FlashcardListCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
