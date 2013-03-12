namespace PluralSightProcessor.Parsers
{
    using System;
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using PluralSightProcessor.Domain;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    class CourseParser
    {
        private static string Uri = "http://www.pluralsight.com/training/Courses";

        const string CourseListXPath = "(//div[@class='courseList'])[{0}]//tr";

        const string CourseTitleXPath = "./td[@class='title']/a";
        const string CourseUtlSuffixXPath = "./td[@class='title']/a";
        const string CourseUtlPrefixXPath = "http://www.pluralsight.com/";

        internal static List<Course> Parse(Library library)
        {
            if (library == null)
            {
                throw new ArgumentNullException("library", "Library parameter must have value");
            }

            //move this to base type for Library and course parser, and to be singlton
            return ParseCourses(library);
        }

        private static List<Course> ParseCourses(Library library, bool parseChaptersAsync = false)
        {
            List<Course> result = new List<Course>();

            HtmlAgilityPack.HtmlDocument webPage = new LibraryDocumentFactory(new Uri(Uri)).GetDocument as HtmlAgilityPack.HtmlDocument;

            if (webPage != null)
            {
                HtmlNodeCollection coursesNodes =
                    webPage.DocumentNode.SelectNodes(String.Format(CourseListXPath, library.Number));

                if (coursesNodes == null)
                {
                    throw new Exception(String.Format("XPath expression does not return any vlaue: {0}, LibraryNumber: {1}, html: {2}", CourseListXPath, library.Number, webPage.DocumentNode.InnerText));
                }

                foreach (var courseNode in coursesNodes)
                {
                    Course course = new Course();
                    course.Name = courseNode.SelectSingleNode(CourseTitleXPath).InnerText.Replace("\\r\\n", "").Trim();

                    string CourseUrl = courseNode.SelectSingleNode(CourseUtlSuffixXPath).Attributes["href"].Value;

                    List<Chapter> chapters = ChapterParser.Parse(new Uri(CourseUtlPrefixXPath + CourseUrl));
                    chapters.ForEach(x => course.Chapters.Add(x));

                    result.Add(course);
                }
            }
            return result;
        }

        internal static Task<List<Course>> ParseAsync(Library library)
        {
            if (library == null)
            {
                throw new ArgumentNullException("library", "Library parameter must have value");
            }

            //move this to base type for Library and course parser, and to be singlton
            Task<List<Course>> task = new Task<List<Course>>(() => { return ParseCourses(library); });
            task.Start();


            return task;
        }
    }
}
