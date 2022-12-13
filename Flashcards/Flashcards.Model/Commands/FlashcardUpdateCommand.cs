using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class FlashcardUpdateCommand : CommandBase, IRequest
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new FlashcardUpdateCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
