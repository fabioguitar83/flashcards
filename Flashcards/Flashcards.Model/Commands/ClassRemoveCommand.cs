using Flashcards.Domain.Validators;
using MediatR;
using Newtonsoft.Json;

namespace Flashcards.Domain.Commands
{
    public class ClassRemoveCommand : CommandBase, IRequest
    {
        public int IdUser { get; set; }
        public override bool IsValid() 
        {
            ValidationResult = new ClassRemoveCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
