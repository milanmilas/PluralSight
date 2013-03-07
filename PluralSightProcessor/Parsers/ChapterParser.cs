namespace PluralSightProcessor.Parsers
{
    using System;
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using PluralSightProcessor.Domain;

    class ChapterParser
    {
        const string ChapterNodeXPath = "//tr[@class='module']";
       // const string ChapterNodeXPath = "//td[@class='title tocModule']";
        const string ChapterTitleXPath = "./td[@class='title tocModule']/div/text()[2]";

        public static List<Chapter> Parse(Uri courseUrl)
        {
            if (courseUrl == null)
            {
                throw new ArgumentNullException("courseUrl");
            }
            List<Chapter> result = new List<Chapter>();

            HtmlDocument webPage = new CourseDocumentFactory(courseUrl).GetDocument as HtmlDocument;

            if (webPage != null)
            {
                HtmlNodeCollection chapterNodes = webPage.DocumentNode.SelectNodes(ChapterNodeXPath);

                int chapterNumber = 1;
                foreach (var chapterNode in chapterNodes)
                {
                    Chapter chapter = new Chapter();

                    String chapterName =
                        chapterNode.SelectSingleNode(ChapterTitleXPath).InnerText.Replace("\\r\\n", "").Trim();
                    chapter.Name = chapterName;
                    chapter.ChapterNumber = chapterNumber++;

                    var res = chapterNode.SelectNodes("../preceding-sibling::node[1]");

                    result.Add(chapter);
                }
            }

            return result;
        }
    }
}
