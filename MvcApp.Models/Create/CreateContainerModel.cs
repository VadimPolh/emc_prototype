using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateContainerModel
    {
        public Container Model { get; set; }

        public CustomRadioButtonList Groups { get; set; }
        public CustomRadioButtonList Lessons { get; set; }
    }
}
