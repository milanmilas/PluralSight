using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace PluralSightProcessor
{
    using PluralSightProcessor.Parsers;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;

    public class  TrainingVideosProcessor 
    {
        public static Configuration Config {get; set;}
        ////Mock
        //public IList<Library> GetLibraryListStub()
        //{
        //    return new ObservableCollection<Library>() { new Library{ Name = ".Net",
        //                                              Courses = new ObservableCollection<Course>{ new Course{ Name = ".NET Reflector by Example",
        //                                                                                          Chapters = new ObservableCollection<Chapter>{ new Chapter{ Name = "Introducing redgate .NET Reflector",
        //                                                                                                                                     Videos = new ObservableCollection<Video>{ new Video{ Name = "Reflector part 1"
        //                                                                                                                                                                       }
        //                                                                                                                                       }
        //                                                                                                                          }
        //                                                                                          }
                                                            
        //                                                                             }
        //                                              }
        //                                 }, new Library{ Name="new"}
        //    };
        //}

        public TrainingVideosProcessor(Configuration config)
        {
            Config = config;
        }

        public TrainingVideosProcessor() : this(new Configuration())
        {
        }

        public IList<Library> GetLibraryListAsync(Uri libraryUri)
        {
            var result = new LibraryParser().Parse(new Uri(Config.LibraryProxyURL + libraryUri.AbsoluteUri));

            //Get Courses
            foreach (var library in result)
            {

                Library cashedLibrary = Config.ExistingList.Where(l => l.Name.Equals(library.Name)).FirstOrDefault();
                GetCourseDetailsAsync(library, cashedLibrary);
            }

            return result;
        }

        private static async void GetCourseDetailsAsync(Library library, Library cashedLibray)
        {
            List<Course> courses =  await CourseParser.ParseAsync(library, cashedLibray);
            AddCourse(library, courses);
        }

        private static void AddCourse(Library library, List<Course> courses)
        {
            if (courses != null && courses.Count != 0)
            {
                courses.ForEach(x => library.Courses.Add(x));
            }
        }


        //TrainingVideoProcessor
        public TrainingVideosProcessor SetExistingList(List<Library> value) {  Config.ExistingList = value;  return this; }
        public TrainingVideosProcessor SetLibraryProxyURL(string value) {  Config.LibraryProxyURL = value;  return this; }
        public TrainingVideosProcessor SetCourseProxyURL(string value) {  Config.CourseProxyURL = value; return this; }

        //LibraryParser
        public TrainingVideosProcessor SetLibraryXPath(string value) {  Config.LibraryXPath = value; return this; }

        public TrainingVideosProcessor SetDivClassTitle(string value) {  Config.DivClassTitle = value; return this; }

        public TrainingVideosProcessor SetNumberOfCoursesXPath(string value) {  Config.NumberOfCoursesXPath = value; return this; }

        //CourseParser
        public TrainingVideosProcessor SetUri(string value) {  Config.Uri = value; return this; }

        public TrainingVideosProcessor SetCourseListXPath(string value) {  Config.CourseListXPath = value; return this; }

        public TrainingVideosProcessor SetCourseTitleXPath(string value) {  Config.CourseTitleXPath = value; return this; }

        public TrainingVideosProcessor SetCourseUtlSuffixXPath(string value) {  Config.CourseTitleXPath = value; return this; }

        public TrainingVideosProcessor SetCourseUtlPrefixXPath(string value) {  Config.CourseUtlPrefixXPath = value; return this; }

        //ChapterParser
        public TrainingVideosProcessor SetChapterNodeXPath(string value) {  Config.ChapterNodeXPath = value; return this; }

        public TrainingVideosProcessor SetChapterTitleXPath(string value) {  Config.ChapterTitleXPath = value; return this; }

        public TrainingVideosProcessor SetVideosXPath(string value) {  Config.VideosXPath = value; return this; }

        public TrainingVideosProcessor SetVideoNameXPath(string value) {  Config.VideoNameXPath = value; return this; }

        //DocumentFactory
        public TrainingVideosProcessor SetNumConcWebRequests(int value) {  Config.NumConcWebRequests = value; return this; }
        
    }
}
