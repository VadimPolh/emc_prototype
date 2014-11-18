using System.Collections.Generic;

namespace MvcApp.Models.Common
{
    public class CustomCheckBoxList
    {
        public CustomCheckBoxList() : this(new List<ChoosableModel<IdNameModel>>())
        {
        }

        public CustomCheckBoxList(List<ChoosableModel<IdNameModel>> items)
        {
            Items = items;
        }

        public string Name { get; set; }

        public List<ChoosableModel<IdNameModel>> Items { get; set; }
    }
}
