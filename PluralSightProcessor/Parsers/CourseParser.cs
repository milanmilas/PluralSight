﻿namespace PluralSightProcessor.Parsers
{
    using System;
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using PluralSightProcessor.Domain;

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
            List<Course> result = new List<Course>();

            //move this to base type for Library and course parser, and to be singlton
            HtmlAgilityPack.HtmlDocument webPage = new LibraryDocumentFactory(new Uri(Uri)).GetDocument as HtmlAgilityPack.HtmlDocument;

            if (webPage != null)
            {
                HtmlNodeCollection coursesNodes =
                    webPage.DocumentNode.SelectNodes(String.Format(CourseListXPath, library.LibraryNumber));

                if (coursesNodes == null)
                {
                    throw new Exception(String.Format("XPath expression does not return any vlaue: {0}, LibraryNumber: {1}, html: {2}", CourseListXPath,library.LibraryNumber, webPage.DocumentNode.InnerText));
                }

                foreach (var courseNode in coursesNodes)
                {
                    Course course = new Course();
                    course.Name = courseNode.SelectSingleNode(CourseTitleXPath).InnerText.Replace("\\r\\n", "").Trim();

                    string CourseUrl = courseNode.SelectSingleNode(CourseUtlSuffixXPath).Attributes["href"].Value;

                    List<Chapter> chapters = ChapterParser.Parse(new Uri(CourseUtlPrefixXPath + CourseUrl));
                    course.Children.AddRange(chapters);

                    result.Add(course);
                }
            }

            return result;
        }
    }
}
