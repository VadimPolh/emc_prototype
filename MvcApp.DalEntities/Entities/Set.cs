using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Set : BaseEntity
    {
        public string Name { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}
