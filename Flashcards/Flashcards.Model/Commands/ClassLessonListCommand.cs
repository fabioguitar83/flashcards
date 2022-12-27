using Flashcards.Domain.Responses;
using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class ClassLessonListCommand : CommandBase, IRequest<IEnumerable<ClassLessonListResponse>>
    {
        public int IdUser { get; set; }
        public override bool IsValid() 
        {
            ValidationResult = new ClassLessonListCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
