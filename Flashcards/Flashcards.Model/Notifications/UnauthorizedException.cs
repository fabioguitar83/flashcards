namespace Flashcards.Domain.Notifications
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string ex) : base(ex) { }
    }
}
