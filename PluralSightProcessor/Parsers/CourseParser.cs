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
        string Uri = TrainingVideosProcessor.Config.Uri;

        string CourseListXPath = TrainingVideosProcessor.Config.CourseListXPath;

        string CourseTitleXPath = TrainingVideosProcessor.Config.CourseTitleXPath;
        string CourseUtlSuffixXPath = TrainingVideosProcessor.Config.CourseUtlSuffixXPath;
        string CourseUtlPrefixXPath = TrainingVideosProcessor.Config.CourseUtlPrefixXPath;


        private List<Course> ParseCourses(Library library, Library cashedLibray = null, bool parseChaptersAsync = false)
        {
            List<Course> result = new List<Course>();

            HtmlDocument webPage = new DocumentFactory(new Uri(Uri)).GetDocument as HtmlDocument;

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
                    Course course = null;
                    string courseName = courseNode.SelectSingleNode(CourseTitleXPath).InnerText.Replace("\\r\\n", "").Trim();

                    if (cashedLibray != null && cashedLibray.Courses.Any(x => x.Name.Equals(courseName)))
                    {
                        course = cashedLibray.Courses.Where(x => x.Name.Equals(courseName)).FirstOrDefault();
                    }else{
                        course = new Course();
                        course.Name = courseName;
                        string CourseUrl = courseNode.SelectSingleNode(CourseUtlSuffixXPath).Attributes["href"].Value;
                        GetChaptersAsync(course, CourseUrl);
                    }


                    result.Add(course);
                }
            }
            return result;
        }

        private async void GetChaptersAsync(Course course, string CourseUrl)
        {

            List<Chapter> chapters = await ChapterParser.ParseAsync(new Uri(CourseUtlPrefixXPath + CourseUrl));
            chapters.ForEach(x => course.Chapters.Add(x));
        }

        internal static Task<List<Course>> ParseAsync(Library library, Library cashedLibray)
        {
            if (library == null)
            {
                throw new ArgumentNullException("library", "Library parameter must have value");
            }

            //move this to base type for Library and course parser, and to be singlton
            Task<List<Course>> task = new Task<List<Course>>( () => new CourseParser().ParseCourses(library,cashedLibray));
            task.Start();

            return task;
        }
    }
}
