using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class ClassAddCommand : CommandBase, IRequest
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public override bool IsValid() 
        {
            ValidationResult = new ClassAddCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
