using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class NamedItem : NotifyPropertyChangedBase
    {
        public String Name { get; set; }
        public List<NamedItem> Children { get; set; }
    }
}
