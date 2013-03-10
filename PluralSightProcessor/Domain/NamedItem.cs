using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class NamedItem : NotifyPropertyChangedBase
    {
        public NamedItem()
        {
            Children = new ObservableCollection<NamedItem>();
        }
        public String Name { get; set; }
        public ObservableCollection<NamedItem> Children { get; set; }
    }
}
