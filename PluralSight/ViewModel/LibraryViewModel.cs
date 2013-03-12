using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSight.ViewModel
{
    public class LibraryViewModel : SelectableItem
    {
        private Library library;

        public String Name {
            get { return library.Name; }
            set { library.Name = value; }
        }

        private ObservableCollection<CourseViewModel> courses = new ObservableCollection<CourseViewModel>();

        public ObservableCollection<CourseViewModel> Courses
        {
            get { return courses; }
            set { courses = value; }
        }

        public LibraryViewModel(Library library)
        {
            this.library = library;
            library.Courses.CollectionChanged += Courses_CollectionChanged;
        }

        public LibraryViewModel()
        {
            library.Courses.CollectionChanged += Courses_CollectionChanged;
        }

        void Courses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {
                courses.Add(new CourseViewModel(item as Course));
            }
        }

        public override List<SelectableItem> GetChildren()
        {
            return courses.ToList<SelectableItem>();
        }
        
    }
}
