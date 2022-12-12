using FluentValidation.Results;
using Newtonsoft.Json;

namespace Flashcards.Domain.Commands
{
    public abstract class CommandBase
    {
        [JsonIgnore]
        public ValidationResult ValidationResult;

        public virtual bool IsValid() 
        {
            return true;
        }
    }
}
