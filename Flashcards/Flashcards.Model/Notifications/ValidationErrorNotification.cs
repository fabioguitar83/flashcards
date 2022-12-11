using FluentValidation.Results;
using MediatR;

namespace Flashcards.Domain.Notifications
{
    public class ValidationErrorNotification: INotification
    {
        public ValidationResult ValidationResult { get; set; }
        public string ValidationMessage { get; set; }

        public ValidationErrorNotification(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
        public ValidationErrorNotification(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }
    }
}
