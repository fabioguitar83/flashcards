using Flashcards.Domain.Entities;
using Flashcards.Domain.Responses;
using Flashcards.Domain.Validators;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class AuthorizeCommand : CommandBase, IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UserGetCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
