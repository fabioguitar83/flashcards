using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class ClassRemoveCommand : CommandBase, IRequest
    {
        public int IdClass { get; set; }
        public override bool IsValid() 
        {
            ValidationResult = new ClassRemoveCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
