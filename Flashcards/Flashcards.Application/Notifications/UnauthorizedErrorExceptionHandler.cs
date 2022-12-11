using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Notifications
{
    public class UnauthorizedErrorExceptionHandler : INotificationHandler<UnauthorizedErrorNotification>
    {
        public Task Handle(UnauthorizedErrorNotification notification, CancellationToken cancellationToken)
        {
            throw new UnauthorizedException(notification.Message);
        }
    }
}
