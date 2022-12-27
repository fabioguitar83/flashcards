using Dapper.FluentMap.Dommel.Mapping;
using Flashcards.Domain.Responses;

namespace Flashcards.Infrastructure.Mappers
{
    public  class LessonResponseMap : DommelEntityMap<LessonResponse>
    {
        public LessonResponseMap()
        {
            Map(x => x.IdLesson).ToColumn("id", false).IsKey();
            Map(x => x.Quantity).ToColumn("qtd_flashcards", false);
            Map(x => x.Name).ToColumn("name", false);
        }
    }
}
