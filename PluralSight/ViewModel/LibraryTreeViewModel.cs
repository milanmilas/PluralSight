using PluralSightProcessor;
using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PluralSight.ViewModel
{
    public class LibraryTreeViewModel
    {
        TrainingVideosProcessor trainingVideosProcessor;
        public IList<LibraryViewModel> libraryViewModelList { get; set; }
        public List<Library> libraryList { get; set; }

        public LibraryTreeViewModel()
        {  

            trainingVideosProcessor = new TrainingVideosProcessor();
            List<Library> cashedList =  DeserializeLibraryXmlList();
            //trainingVideosProcessor.GetLibraryListStub().ToList();//
            libraryList = trainingVideosProcessor
                                .SetExistingList(cashedList)
                                .GetLibraryListAsync(new Uri("http://www.get-access.appspot.com/" + "www.pluralsight.com/training/Courses"))
                                .ToList();

            //http://www.surfhidden.com/surfhidden.php?u=http%3A%2F%2Fwww.google.com
            //.GetLibraryListAsync(new Uri("http://anonymouse.org/cgi-bin/anon-www.cgi/" + "http://www.pluralsight.com/training/Courses"))
            libraryViewModelList = libraryList.Select(x => new LibraryViewModel(x)).ToList<LibraryViewModel>();
        }

        public void SaveDictionary(string Path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Library>));
            // Create an XmlTextWriter using a FileStream.
            Stream fs = new FileStream(Path, FileMode.Create);
            XmlWriter writer =
            new XmlTextWriter(fs, Encoding.Unicode);

            var library = libraryViewModelList.Select(x => x.Library).ToList();

            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, library);

            writer.Close();
        }

        private List<Library> DeserializeLibraryXmlList()
        {
            List<Library> result = new List<Library>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Library>));

            StreamReader reader = new StreamReader(@"LibraryList.xml");
            object deserialized = serializer.Deserialize(reader.BaseStream);
            reader.Close();

            return (List<Library>)deserialized;
        }

    }
}
