using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class LessonAddCommand: CommandBase, IRequest
    {
        public int IdClass { get; set; }
        public string Name { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new LessonAddCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
