using System;
using HtmlAgilityPack;

namespace PluralSightProcessor
{
    public class LibraryDocumentFactory : ILibraryDocumentFactory
    {
        protected Uri Uri { get; set; }

        private static volatile object htmlDocument;

        private static readonly object SyncRoot = new Object();

        public LibraryDocumentFactory(Uri uri)
        {
            Uri = uri;
        }

        public object GetDocument
        {
            get
            {
                if (htmlDocument == null)
                {
                    lock (SyncRoot)
                    {
                        if (htmlDocument == null) htmlDocument = new HtmlWeb().Load(Uri.ToString());
                    }
                }

                return htmlDocument;
            }
        }
    }

}
