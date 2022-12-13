using Dapper.FluentMap.Dommel.Mapping;
using Flashcards.Domain.Entities;

namespace Flashcards.Infrastructure.Mappers
{
    public  class FlashcardEntityMap : DommelEntityMap<FlashcardEntity>
    {
        public FlashcardEntityMap()
        {
            ToTable("flashcard");
            Map(x => x.Id).ToColumn("id", false).IsKey();
            Map(x => x.IdLesson).ToColumn("id_lesson", false);
            Map(x => x.Back).ToColumn("back", false);
            Map(x => x.Front).ToColumn("front", false);
        }
    }
}
