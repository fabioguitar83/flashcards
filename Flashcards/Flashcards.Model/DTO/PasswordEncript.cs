namespace Flashcards.Domain.DTO
{
    public class PasswordEncript
    {
        public PasswordEncript(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }

        public string Password { get; set; }
        public string Salt { get; set; }

    }
}
