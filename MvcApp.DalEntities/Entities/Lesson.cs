using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual LessonType LessonType { get; set; }
        public virtual List<Container> Containers { get; set; }
        public virtual List<Teacher> Teachers { get; set; }
    }
}
