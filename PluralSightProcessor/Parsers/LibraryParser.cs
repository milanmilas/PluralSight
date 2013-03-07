namespace PluralSightProcessor.Parsers
{
    using System;
    using System.Collections.Generic;

    using HtmlAgilityPack;

    using PluralSightProcessor.Domain;

    class LibraryParser
    {
        const string LibraryXPath = "//div[@class='categoryHeader ']";
        const string DivClassTitle = "./div[@class='title']";

        public static List<Library> Parse(Uri libraryUri)
        {
            if (libraryUri == null)
            {
                throw new ArgumentNullException("libraryUri", "Parameter libraryUri must have value");
            }
            List<Library> result = new List<Library>();

            HtmlDocument webPage = new LibraryDocumentFactory(libraryUri).GetDocument as HtmlDocument;

            if (webPage != null)
            {
                HtmlNodeCollection libraryNodes = webPage.DocumentNode.SelectNodes(LibraryXPath);

                int LibraryNumber = 1;
                foreach (var libraryNode in libraryNodes)
                {
                    Library library = new Library();

                    String libraryName =
                        libraryNode.SelectSingleNode(DivClassTitle).InnerText.Replace("\\r\\n", "").Trim();
                    library.Name = libraryName;
                    library.LibraryNumber = LibraryNumber++;

                    result.Add(library);
                }
            }
            return result;
        }
    }
}
