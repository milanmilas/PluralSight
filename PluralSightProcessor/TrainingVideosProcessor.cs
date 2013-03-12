using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace PluralSightProcessor
{
    using PluralSightProcessor.Parsers;
    using System.Threading.Tasks;

    public class TrainingVideosProcessor 
    {
        //Mock
        //public IList<Library> GetLibraryList()
        //{
        //    return new List<Library>() { new Library{ Name = ".Net",
        //                                              Children = new List<NamedItem>{ new Course{ Name = ".NET Reflector by Example",
        //                                                                                          Children = new List<NamedItem>{ new Chapter{ Name = "Introducing redgate .NET Reflector",
        //                                                                                                                                       Children = new List<NamedItem>{ new Video{ Name = "Reflector part 1"
        //                                                                                                                                                                       }
        //                                                                                                                                       }
        //                                                                                                                          }
        //                                                                                          }
                                                            
        //                                                                             }
        //                                              }
        //                                 }, new Library{ Name="new"}
        //    };
        //}

        public IList<Library> GetLibraryList(Uri libraryUri)
        {
            var result = LibraryParser.Parse(libraryUri);

            //Get Courses
            foreach (var library in result)
            {
                GetCourseDetails(library);
            }

            return result;
        }

        public IList<Library> GetLibraryListAsync(Uri libraryUri)
        {
            var result = LibraryParser.Parse(libraryUri);

            //Get Courses
            foreach (var library in result)
            {
                GetCourseDetailsAsync(library);
            }

            return result;
        }

        private static void GetCourseDetails(Library library)
        {
            List<Course> courses = CourseParser.Parse(library);

            AddCourse(library, courses);
        }

        private static async void GetCourseDetailsAsync(Library library)
        {
            List<Course> courses =  await CourseParser.ParseAsync(library);
            AddCourse(library, courses);
        }

        private static void AddCourse(Library library, List<Course> courses)
        {
            if (courses != null && courses.Count != 0)
            {
                courses.ForEach(x => library.Courses.Add(x));
            }
        }
    }
}
