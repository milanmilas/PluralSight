using System;
using HtmlAgilityPack;

namespace PluralSightProcessor
{
    public class CourseDocumentFactory : ICourseDocumentFactory
    {
        protected static Uri Uri { get; set; }

        private static volatile object htmlDocument;

        private static readonly object SyncRoot = new Object();

        public CourseDocumentFactory(Uri uri)
        {
            if (Uri != null && !uri.AbsoluteUri.Equals(Uri.AbsoluteUri))
            {
                htmlDocument = null;
            } 

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
