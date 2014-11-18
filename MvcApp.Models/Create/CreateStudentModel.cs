using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateStudentModel
    {
        public Student Model { get; set; }

        public CustomRadioButtonList Groups { get; set; }
    }
}
