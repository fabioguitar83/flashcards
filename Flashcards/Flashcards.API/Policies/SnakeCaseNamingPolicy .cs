using Flashcards.API.Utils;
using System.Text.Json;

namespace Flashcards.API.Policies
{
    public class SnakeCaseNamingPolicy: JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}
