using Flashcards.Domain.Commands;
using FluentValidation;

namespace Flashcards.Domain.Validators
{
    public class ClassLessonListCommandValidator : AbstractValidator<ClassLessonListCommand> 
    {
        public ClassLessonListCommandValidator()
        {
            RuleFor(x => x.IdUser)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
