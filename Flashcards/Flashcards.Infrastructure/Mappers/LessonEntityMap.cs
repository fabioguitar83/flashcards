using Dapper.FluentMap.Dommel.Mapping;
using Flashcards.Domain.Entities;

namespace Flashcards.Infrastructure.Mappers
{
    public  class LessonEntityMap : DommelEntityMap<LessonEntity>
    {
        public LessonEntityMap()
        {
            ToTable("class");
            Map(x => x.Id).ToColumn("id", false).IsKey();
            Map(x => x.IdClass).ToColumn("id_class", false);
            Map(x => x.Name).ToColumn("name", false);
        }
    }
}
