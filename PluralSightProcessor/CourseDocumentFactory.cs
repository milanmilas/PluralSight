using System;
using HtmlAgilityPack;

namespace PluralSightProcessor
{
    using System.Collections.Generic;

    public class CourseDocumentFactory : ICourseDocumentFactory
    {
        protected Uri Uri { get; set; }

        private static Dictionary<Uri, HtmlDocument> WebPagesDictionay = new Dictionary<Uri,HtmlDocument>();

        private static readonly object SyncRoot = new Object();

        public CourseDocumentFactory(Uri uri)
        {
            Uri = uri;
        }

        public object GetDocument
        {
            get
            {
                if (!WebPagesDictionay.ContainsKey(Uri))
                {
                    lock (SyncRoot)
                    {
                        if (!WebPagesDictionay.ContainsKey(Uri)) WebPagesDictionay.Add(Uri, new HtmlWeb().Load(Uri.ToString()));
                    }
                }

                return WebPagesDictionay[Uri];
            }
        }
    }

}
