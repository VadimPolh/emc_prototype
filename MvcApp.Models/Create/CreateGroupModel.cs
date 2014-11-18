using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateGroupModel
    {
        public Group Model { get; set; }

        public CustomRadioButtonList Courses { get; set; }
        public CustomRadioButtonList Teachers { get; set; }

        public CustomCheckBoxList Students { get; set; }
        public CustomCheckBoxList Containers { get; set; }
    }
}
