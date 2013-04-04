namespace PluralSightProcessor.Parsers
{
    using System;
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using PluralSightProcessor.Domain;

    class LibraryParser
    {
        string LibraryXPath = TrainingVideosProcessor.Config.LibraryXPath;
        string DivClassTitleXPath = TrainingVideosProcessor.Config.DivClassTitleXPath;
        string NumberOfCoursesXPath = TrainingVideosProcessor.Config.NumberOfCoursesXPath;

        public List<Library> Parse(Uri libraryUri)
        {
            if (libraryUri == null)
            {
                throw new ArgumentNullException("libraryUri", "Parameter libraryUri must have value");
            }
            List<Library> result = new List<Library>();

            HtmlDocument webPage = new DocumentFactory(libraryUri).GetDocument as HtmlDocument;

            if (webPage != null)
            {
                HtmlNodeCollection libraryNodes = webPage.DocumentNode.SelectNodes(LibraryXPath);

                int LibraryNumber = 1;
                foreach (var libraryNode in libraryNodes)
                {
                    Library library = new Library();

                    String libraryName =
                        libraryNode.SelectSingleNode(DivClassTitleXPath).InnerText.Replace("\\r\\n", "").Trim();
                    library.Name = libraryName;
                    library.Number = LibraryNumber++;
                    String numCours = libraryNode.SelectSingleNode(NumberOfCoursesXPath).InnerText.Replace("\\r\\n", "").Replace("courses)", "")
                                                                                                    .Replace("course)", "").Replace("(", "").Trim();
                    int numCourseNumber;
                    int.TryParse(numCours, out numCourseNumber);
                    library.NumberOfCourses = numCourseNumber;

                    result.Add(library);
                }
            }
            return result;
        }
    }
}
