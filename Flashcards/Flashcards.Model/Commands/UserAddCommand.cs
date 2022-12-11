using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class UserAddCommand: CommandBase, IRequest
    {
        public string Email { get ; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public override bool IsValid() 
        {
            ValidationResult = new UserAddCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
