namespace Flashcards.Domain.Responses
{
    public class UserGetResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
