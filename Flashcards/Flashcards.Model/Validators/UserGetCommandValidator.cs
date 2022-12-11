using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class UserGetCommandValidator : AbstractValidator<AuthorizeCommand>
    {
        public UserGetCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")                
                .EmailAddress().WithMessage("Campo {PropertyName} inválido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
       
    }
}
