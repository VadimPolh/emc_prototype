using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateTeacherModel
    {
        public Teacher Model { get; set; }

        public CustomCheckBoxList Lessons { get; set; }
        public CustomCheckBoxList Groups { get; set; }
    }
}
