using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateSetModel
    {
        public Set Model { get; set; }

        public CustomRadioButtonList Specialties { get; set; }
        public CustomCheckBoxList Courses { get; set; }
    }
}
