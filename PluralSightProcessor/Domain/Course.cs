using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class Course
    {
        public String Name { get; set; }

        private ObservableCollection<Chapter> chapters = new ObservableCollection<Chapter>();

        public ObservableCollection<Chapter> Chapters
        {
            get { return chapters; }
            set { chapters = value; }
        }
        
    }
}
