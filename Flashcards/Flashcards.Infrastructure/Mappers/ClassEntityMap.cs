using Dapper.FluentMap.Dommel.Mapping;
using Flashcards.Domain.Entities;

namespace Flashcards.Infrastructure.Mappers
{
    public  class ClassEntityMap: DommelEntityMap<ClassEntity>
    {
        public ClassEntityMap()
        {
            ToTable("class");
            Map(x => x.Id).ToColumn("id", false).IsKey();
            Map(x => x.IdUser).ToColumn("id_user", false);
            Map(x => x.Name).ToColumn("name", false);
        }
    }
}
