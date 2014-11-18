using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class LessonType : BaseEntity
    {
        public string Name { get; set; }
        public string Topic { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
