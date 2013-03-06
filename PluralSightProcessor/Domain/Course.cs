using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class Course : SelectableItem
    {
        public List<Chapter> Chapters { get; set; }
    }
}
