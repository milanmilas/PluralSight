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
        private int currentNumberOfCourses;
        private int progress;

        public Library Library {
            get { return library; }
            set { library = value; }
        }

        public String Name {
            get { return library.Name; }
            set { library.Name = value; }
        }

        public int NumberOfCourses
        {
            get { return library.NumberOfCourses; }
        }

        public int CurrentNumberOfCourses
        {
            get {return currentNumberOfCourses;}
            set { currentNumberOfCourses = value; Progress = (NumberOfCourses != 0) ? (currentNumberOfCourses * 100/NumberOfCourses) : 0; }
        }

        public int Progress { get { return progress; } set { progress = value; OnPropertyChanged(() => this.Progress); } }

        private ObservableCollection<CourseViewModel> courses = new ObservableCollection<CourseViewModel>();

        public ObservableCollection<CourseViewModel> Courses
        {
            get { return courses; }
            set { courses = value; }
        }

        public LibraryViewModel(Library library)
        {
            Statistics.Singlton.NumberOfLibraries++;
            this.library = library;
            library.Courses.CollectionChanged += Courses_CollectionChanged;
            lock (library)
            {
                foreach (var item in library.Courses)
                {
                    CurrentNumberOfCourses++;
                    courses.Add(new CourseViewModel(item as Course));
                }
            }
        }

        public LibraryViewModel()
        {
            library.Courses.CollectionChanged += Courses_CollectionChanged;
        }

        void Courses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {
                CurrentNumberOfCourses++;
                courses.Add(new CourseViewModel(item as Course));
            }
        }

        public override List<SelectableItem> GetChildren()
        {
            return courses.ToList<SelectableItem>();
        }
        
    }
}
