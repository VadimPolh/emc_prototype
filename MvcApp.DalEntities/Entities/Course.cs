using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Course : BaseEntity
    {
        public int Number { get; set; }

        public virtual Set Set { get; set; }
        public virtual List<Group> Groups { get; set; }
        public virtual List<Subject> Subjects { get; set; }
    }
}
