using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public abstract class SelectableItem : NotifyPropertyChangedBase
    {
        public abstract List<SelectableItem> GetChildren();

        private bool isSelected = false;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (GetChildren() != null) SelectChildred(value);
                OnPropertyChanged(() => IsSelected);
            }
        }

        public void SelectChildred(bool selected)
        {
            foreach (var child in GetChildren())
            {
                if (child is SelectableItem)
                {
                    (child as SelectableItem).IsSelected = selected;
                }
            }
        }
    }
}
