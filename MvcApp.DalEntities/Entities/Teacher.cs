using System.Collections.Generic;
using System.ComponentModel;

namespace MvcApp.DalEntities.Entities
{
    public class Teacher : BaseEntity
    {
        [DisplayName("ФИО")]
        public string Fio { get; set; }

        [DisplayName("Пароль")]
        public string Pass { get; set; }

        [DisplayName("Стаж")]
        public int Exp { get; set; }

        public virtual List<Lesson> Lessons { get; set; }
        public virtual List<Group> Groups { get; set; } 
    }
}
