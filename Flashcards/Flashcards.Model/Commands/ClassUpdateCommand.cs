using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class ClassUpdateCommand : CommandBase, IRequest
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public override bool IsValid() 
        {
            ValidationResult = new ClassUpdateCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
