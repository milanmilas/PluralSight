using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class Chapter : SelectableItem
    {
        public List<Video> Videos { get; set; }

        public int ChapterNumber { get; set; }
    }
}
