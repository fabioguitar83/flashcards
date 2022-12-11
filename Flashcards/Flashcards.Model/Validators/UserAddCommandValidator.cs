using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class UserAddCommandValidator : AbstractValidator<UserAddCommand>
    {
        public UserAddCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .EmailAddress().WithMessage("Campo {PropertyName} inválido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(6, 50).WithMessage("Campo {PropertyName} deve ter entre 6 e 10 caracteres")
                .Must(ValidatePasswordNumber).WithMessage("Campo {PropertyName} deve ter conter pelo menos um número")
                .Must(ValidatePasswordUpperChar).WithMessage("Campo {PropertyName} deve ter conter pelo menos uma letra maiuscula")
                .Must(ValidatePasswordLowerChar).WithMessage("Campo {PropertyName} deve ter conter pelo menos uma letra minúscula");

        }

        private bool ValidatePasswordNumber(string arg)
        {
            var numbers = "1234567890";

            foreach (var item in numbers.ToList())
            {
                if (arg.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValidatePasswordUpperChar(string arg)
        {
            var upperChars = "ZXCVBNMASDFGHJKLÇQWERTYUIOP";

            foreach (var item in upperChars.ToList())
            {
                if (arg.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
        private bool ValidatePasswordLowerChar(string arg)
        {
            var upperChars = "zxcvbnmasdfghjklçqwertyuiop";

            foreach (var item in upperChars.ToList())
            {
                if (arg.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
