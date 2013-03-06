using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class SelectableItem : NamedItem
    {
        private bool isSelected = false;

        public bool IsSelected {
            get { return isSelected; } 
            set{
                isSelected = value;
                if(Children != null)  SelectChildred(value);
                OnPropertyChanged(() => IsSelected);
            } 
        }

        public void SelectChildred(bool selected)
        {
            foreach (var child in Children)
            {
                if (child is SelectableItem)
                {
                    (child as SelectableItem).IsSelected = selected;
                }
            }
        }
    }
}
