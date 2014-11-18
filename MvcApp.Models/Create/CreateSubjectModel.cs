using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateSubjectModel
    {
        public Subject Model { get; set; }

        public CustomCheckBoxList Courses { get; set; }
        public CustomCheckBoxList Lessons { get; set; }
    }
}
