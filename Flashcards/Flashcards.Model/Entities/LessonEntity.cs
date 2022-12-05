using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Model.Entities
{
    public class LessonEntity
    {
        public int Id { get; set; }
        public int IdClass { get; set; }
        public string Name { get; set; }
    }
}
