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

        private string libraryURL = "www.pluralsight.com/training/Courses";
        public string LibraryURL { get { return libraryURL; } set { libraryURL = value; } }

        private string libraryProxyURL = "http://www.get-access.appspot.com/";
        public string LibraryProxyURL { get { return libraryProxyURL; } set { libraryProxyURL = value; } }

        //LibraryParser
        private string libraryXPath = "//div[@class='categoryHeader ']";
        public string LibraryXPath { get { return libraryXPath; } set { libraryXPath = value; } }

        private string divClassTitleXPath = "./div[@class='title']";
        public  string DivClassTitleXPath { get { return divClassTitleXPath; } set { divClassTitleXPath = value; } }

        private string numberOfCoursesXPath = "./div[@class='courseCount']";
        public  string NumberOfCoursesXPath { get { return numberOfCoursesXPath; } set { numberOfCoursesXPath = value; } }

        //CourseParser
        private string courseListXPath = "(//div[@class='courseList'])[{0}]//tr";
        public  string CourseListXPath { get { return courseListXPath; } set { courseListXPath = value; } }

        private string courseTitleXPath = "./td[@class='title']/a";
        public  string CourseTitleXPath { get { return courseTitleXPath; } set { courseTitleXPath = value; } }

        private string courseUtlSuffixXPath = "./td[@class='title']/a";
        public string CourseUtlSuffixXPath { get { return courseUtlSuffixXPath; } set { courseUtlSuffixXPath = value; } }

        private string courseProxyURL = "http://www.get-access.appspot.com";
        public string CourseProxyURL { get { return courseProxyURL; } set { courseProxyURL = value; } }

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
