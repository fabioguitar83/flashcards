using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class LessonUpdateCommand : CommandBase, IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new LessonUpdateCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
