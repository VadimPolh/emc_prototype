using System.Collections.Generic;

namespace MvcApp.DalEntities.Entities
{
    public class Group : BaseEntity
    {
        public string GroupNumber { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual List<Container> Containers { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
