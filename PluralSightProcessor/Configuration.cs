using PluralSightProcessor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor
{
    public  class Configuration
    {
        //TrainingVideoProcessor
        public IList<Library> ExistingList { get; set; }
        public string LibraryProxyURL { get; set; }
        public string CourseProxyURL { get; set; } 

        //LibraryParser
        private string libraryXPath = "//div[@class='categoryHeader ']";
        public string LibraryXPath { get { return libraryXPath; } set { libraryXPath = value; } }

        private string divClassTitle = "./div[@class='title']";
        public  string DivClassTitle { get { return divClassTitle; } set { divClassTitle = value; } }

        private string numberOfCoursesXPath = "./div[@class='courseCount']";
        public  string NumberOfCoursesXPath { get { return numberOfCoursesXPath; } set { numberOfCoursesXPath = value; } }

        //CourseParser
        private string uri = "http://www.get-access.appspot.com/www.pluralsight.com/training/Courses";
        public  string Uri { get { return uri; } set { uri = value; } }

        private string courseListXPath = "(//div[@class='courseList'])[{0}]//tr";
        public  string CourseListXPath { get { return courseListXPath; } set { courseListXPath = value; } }

        private string courseTitleXPath = "./td[@class='title']/a";
        public  string CourseTitleXPath { get { return courseTitleXPath; } set { courseTitleXPath = value; } }

        private string courseUtlSuffixXPath = "./td[@class='title']/a";
        public string CourseUtlSuffixXPath { get { return courseTitleXPath; } set { courseTitleXPath = value; } }

        private string courseUtlPrefixXPath = "http://www.get-access.appspot.com";
        public string CourseUtlPrefixXPath { get { return courseUtlPrefixXPath; } set { courseUtlPrefixXPath = value; } }

        //ChapterParser
        private string chapterNodeXPath = "//tr[@class='module']";
        public  string ChapterNodeXPath { get { return chapterNodeXPath; } set { chapterNodeXPath = value; } }

        private string chapterTitleXPath = "./td[@class='title tocModule']/div/text()[2]";
        public string ChapterTitleXPath { get { return chapterTitleXPath; } set { chapterTitleXPath = value; } }

        private string videosXPath = "../tr[@class='tocClips' and preceding-sibling::tr[@id][1][@id='{0}']]";
        public string VideosXPath { get { return videosXPath; } set { videosXPath = value; } }

        private string videoNameXPath = "./td[@class='clipTitle']/div";
        public string VideoNameXPath { get { return videoNameXPath; } set { videoNameXPath = value; } }

        //DocumentFactory
        private int numConcWebRequests = 10;
        public int NumConcWebRequests { get { return numConcWebRequests; } set { numConcWebRequests = value; } }
    }
}
