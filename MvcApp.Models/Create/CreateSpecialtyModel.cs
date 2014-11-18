using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateSpecialtyModel
    {
        public Specialty Model { get; set; }
        
        public CustomRadioButtonList Branches { get; set; }
        public CustomCheckBoxList Sets { get; set; }
    }
}
