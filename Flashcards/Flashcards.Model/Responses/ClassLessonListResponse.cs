namespace Flashcards.Domain.Responses
{
    public class ClassLessonListResponse
    {
        public int IdClass { get; set; }
        public string Name{ get; set; }

        public IList<LessonResponse> Lessons{ get; set; }
    }

    public class LessonResponse 
    {
        public int IdLesson { get; set;}
        public string Name { get; set;}
        public int Quantity { get; set; }
    }
}
