using Flashcards.Application.Interfaces;
using Flashcards.Application.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Flashcards.Application.Services
{
    public class UserContextService: IUserContextService
    {
        private readonly IHttpContextAccessor _context;

        public UserContextService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public UserContextModel GetUserContext()
        {
            UserContextModel userContextModel = null;
            var identity = _context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                var name = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value;
                var email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value;
                var id = claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;

                userContextModel = new UserContextModel(Convert.ToInt32(id), name, email);
            }

            return userContextModel;
        }
    }
}
