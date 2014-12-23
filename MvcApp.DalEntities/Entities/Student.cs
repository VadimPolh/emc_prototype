using System.Collections.Generic;
using System.ComponentModel;

namespace MvcApp.DalEntities.Entities
{
    public class Student : BaseEntity
    {
        public string Fio { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public virtual Group Group { get; set; }
    }
}
