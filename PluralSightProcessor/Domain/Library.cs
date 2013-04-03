using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class Library
    {
        public String Name { get; set; }

        public int Number { get; set; }

        public int NumberOfCourses { get; set; }

        private ObservableCollection<Course> courses = new ObservableCollection<Course>();

        public ObservableCollection<Course> Courses
        {
            get { return courses; }
            set { courses = value; }
        }
    }
}
