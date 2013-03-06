using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor
{
    public class TrainingVideosProcessor : PluralSightProcessor.ITrainingVideosProcessor
    {
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
    }
}
