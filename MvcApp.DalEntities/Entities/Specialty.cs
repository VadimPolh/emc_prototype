using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public string Mentor { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual List<Set> Sets { get; set; }
    }
}
