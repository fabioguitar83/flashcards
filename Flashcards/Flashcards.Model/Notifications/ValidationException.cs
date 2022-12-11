namespace Flashcards.Domain.Notifications
{
    public class ValidationException : Exception
    {
        public ValidationException(string ex) : base(ex) { }
    }
}
