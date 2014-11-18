using System;

namespace MvcApp.DalEntities.Entities
{
    public class Container : BaseEntity
    {
        public DateTime Date { get; set; }
        public int PairNumber { get; set; }
        public string Room { get; set; }

        public virtual Group Group { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
