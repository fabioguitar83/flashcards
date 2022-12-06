using Flashcards.Domain.DTO;

namespace Flashcards.Infrastructure.BCript
{
    public static class BCript
    {
        public static PasswordEncript Encript(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordEncript = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return new PasswordEncript(passwordEncript, salt);
        }

        public static bool Verify(string password, string hash, string salt)
        {
            var passwordEncript = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return (passwordEncript == hash);
        }
    }
}
