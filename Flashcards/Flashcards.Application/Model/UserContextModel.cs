namespace Flashcards.Application.Model
{
    public class UserContextModel
    {
        public UserContextModel(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
