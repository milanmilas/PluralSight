using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class Chapter
    {
        public String Name { get; set; }

        public int Number { get; set; }

        private ObservableCollection<Video> videos = new ObservableCollection<Video>();

        public ObservableCollection<Video> Videos
        {
            get { return videos; }
            set { videos = value; }
        }
        
    }
}
