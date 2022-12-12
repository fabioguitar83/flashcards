using Flashcards.Domain.Responses;
using Flashcards.Domain.Validators;
using MediatR;
using Newtonsoft.Json;

namespace Flashcards.Domain.Commands
{
    public class ClassListCommand : CommandBase, IRequest<IEnumerable<ClassListResponse>>
    {
        public int IdUser { get; set; }
        public override bool IsValid() 
        {
            ValidationResult = new ClassListCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
