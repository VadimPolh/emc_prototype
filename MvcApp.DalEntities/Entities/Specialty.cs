using System.Collections.Generic;
using System.ComponentModel;

namespace MvcApp.DalEntities.Entities
{
    public class Specialty : BaseEntity
    {
        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Куратор")]
        public string Mentor { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual List<Set> Sets { get; set; }
    }
}
