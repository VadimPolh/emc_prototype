using MvcApp.DalEntities.Entities;
using MvcApp.Models.Common;

namespace MvcApp.Models.Create
{
    public class CreateBranchModel
    {
        public Branch Model { get; set; }

        public CustomCheckBoxList Specialities { get; set; }
    }
}
