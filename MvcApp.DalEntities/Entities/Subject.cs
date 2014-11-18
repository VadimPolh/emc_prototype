using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public string Book { get; set; }
        public string Plan { get; set; }
        public string Okr { get; set; }
        public string Test { get; set; }
        public int HoursNumber { get; set; }

        public virtual List<Course> Courses { get; set; }
        public virtual List<Lesson> Lessons { get; set; }
    }
}
