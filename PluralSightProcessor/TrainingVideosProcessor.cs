using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace PluralSightProcessor
{
    using PluralSightProcessor.Parsers;

    public class TrainingVideosProcessor : PluralSightProcessor.ITrainingVideosProcessor
    {
        //Mock
        public IList<Library> GetLibraryList()
        {
            return new List<Library>() { new Library{ Name = ".Net",
                                                      Children = new List<NamedItem>{ new Course{ Name = ".NET Reflector by Example",
                                                                                                  Children = new List<NamedItem>{ new Chapter{ Name = "Introducing redgate .NET Reflector",
                                                                                                                                               Children = new List<NamedItem>{ new Video{ Name = "Reflector part 1"
                                                                                                                                                                               }
                                                                                                                                               }
                                                                                                                                  }
                                                                                                  }
                                                            
                                                                                     }
                                                      }
                                         }, new Library{ Name="new"}
            };
        }

        public IList<Library> GetLibraryList(Uri libraryUri)
        {
            var result = LibraryParser.Parse(libraryUri);

            //Get Courses
            foreach (var library in result)
            {
                List<Course> courses = CourseParser.Parse(library);

                if (courses != null && courses.Count != 0)
                {
                    library.Courses.AddRange(courses);
                    library.Children.AddRange(courses);
                }
            }

            return result;
        }


    }
}
