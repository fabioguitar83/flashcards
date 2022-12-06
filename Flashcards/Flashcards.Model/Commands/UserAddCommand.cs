using Flashcards.Domain.Entities;
using MediatR;

namespace Flashcards.Domain.Commands
{
    public class UserAddCommand: IRequest<string>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
