using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateLessonModel
    {
        public Lesson Model { get; set; }

        public CustomRadioButtonList LessonTypes { get; set; }
        public CustomRadioButtonList Subjects { get; set; }

        public CustomCheckBoxList Containers { get; set; }
        public CustomCheckBoxList Teachers { get; set; }
    }
}
