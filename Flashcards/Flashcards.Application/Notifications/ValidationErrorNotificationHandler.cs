using Flashcards.Domain.Notifications;
using MediatR;
using System.Text;

namespace Flashcards.Application.Notifications
{
    public class ValidationErrorNotificationHandler : INotificationHandler<ValidationErrorNotification>
    {
        public async Task Handle(ValidationErrorNotification notification, CancellationToken cancellationToken)
        {
            var errors = new StringBuilder();

            if (notification.ValidationResult == null)
            {
                errors.AppendLine(notification.ValidationMessage);
            }
            else
            {
                notification.ValidationResult.Errors.ForEach(error =>
                {
                    errors.AppendLine(error.ErrorMessage);
                });
            }

            throw new ValidationException(errors.ToString());
        }
    }
}
