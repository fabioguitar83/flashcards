using Flashcards.Domain.Responses;
using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class LessonListCommand: CommandBase, IRequest<IEnumerable<LessonListResponse>>
    {
        public int IdClass { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new LessonListCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
