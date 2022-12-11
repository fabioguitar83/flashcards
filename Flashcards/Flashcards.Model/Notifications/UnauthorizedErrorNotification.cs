using MediatR;

namespace Flashcards.Domain.Notifications
{
    public class UnauthorizedErrorNotification : INotification
    {
        public readonly string Message;

        public UnauthorizedErrorNotification(string message) 
        {
            Message = message;
        }
    }
}
