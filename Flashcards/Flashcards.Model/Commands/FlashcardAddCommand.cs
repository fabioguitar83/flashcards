using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class FlashcardAddCommand : CommandBase, IRequest
    {
        public int IdLesson { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new FlashcardAddCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
