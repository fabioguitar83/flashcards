using FluentValidation.Results;

namespace Flashcards.Domain.Commands
{
    public abstract class CommandBase
    {
        public ValidationResult ValidationResult;

        public virtual bool IsValid() 
        {
            return true;
        }
    }
}
