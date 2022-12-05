namespace Flashcards.Domain.Entities
{
    public class FlashcardEntity
    {
        public int Id { get; set; }
        public int IdLesson { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
    }
}
