using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateLessonTypeModel
    {
        public LessonType Model { get; set; }

        public CustomCheckBoxList Lessons { get; set; }
    }
}
