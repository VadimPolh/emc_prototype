using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateCourseModel
    {
        public Course Model { get; set; }

        public CustomRadioButtonList Sets { get; set; }

        public CustomCheckBoxList Subjects { get; set; }
        public CustomCheckBoxList Groups { get; set; }
    }
}
