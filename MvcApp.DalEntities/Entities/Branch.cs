using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Specialty> Specialties { get; set; }
    }
}
