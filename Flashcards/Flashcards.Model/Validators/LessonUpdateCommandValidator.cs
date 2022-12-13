using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class LessonUpdateCommandValidator : AbstractValidator<LessonUpdateCommand>
    {
        public LessonUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 200).WithMessage("O campo {PropertyName} deve ter entre 2 e 200 caracteres");
        }
 
    }
}
