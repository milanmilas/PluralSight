using PluralSightProcessor;
using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSight.ViewModel
{
    public class LibraryTreeViewModel
    {
        TrainingVideosProcessor trainingVideosProcessor;
        public IList<Library> libraryList { get; set;}

        public LibraryTreeViewModel()
        {
            trainingVideosProcessor = new TrainingVideosProcessor();
            libraryList = trainingVideosProcessor.GetLibraryList(new Uri("http://www.pluralsight.com/training/Courses"));
        }
    }
}
