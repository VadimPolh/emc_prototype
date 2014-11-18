using System.Collections.Generic;
using System.Linq;

namespace MvcApp.Models.Common
{
    public class CustomRadioButtonList
    {
        public CustomRadioButtonList()
        {
        }

        public CustomRadioButtonList(List<ChoosableModel<IdNameModel>> items)
        {
            Items = items;
            var fod = items.FirstOrDefault(x => x.IsSelected);
            SelectedValue = fod != null ? fod.Model.Id : 0;
        }

        public string Name { get; set; }

        public List<ChoosableModel<IdNameModel>> Items { get; set; }
        public int SelectedValue { get; set; }
    }
}
