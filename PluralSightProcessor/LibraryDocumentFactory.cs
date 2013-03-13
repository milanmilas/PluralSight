using System;
using HtmlAgilityPack;
using System.Threading;
using System.Collections.Generic;

namespace PluralSightProcessor
{
    public class DocumentFactory : ILibraryDocumentFactory
    {
        protected Uri Uri { get; set; }

        private static Dictionary<Uri, ManualResetEventSlim> UriBarierDictionary = new Dictionary<Uri, ManualResetEventSlim>();

        private static Object barierDicSync = new Object();

        private static Dictionary<Uri, HtmlDocument> HtmlDocumentDictionary = new Dictionary<Uri, HtmlDocument>();

        private static readonly object SyncRoot = new Object();

        public DocumentFactory(Uri uri)
        {
            Uri = uri;
        }

        public object GetDocument
        {
            get
            {
                ManualResetEventSlim barier = null;

                if (!UriBarierDictionary.ContainsKey(Uri))
                {
                    bool downloadPage = false;

                    lock (barierDicSync)
                    {
                        if (!UriBarierDictionary.ContainsKey(Uri))
                        {
                            barier = new ManualResetEventSlim(false);
                            UriBarierDictionary.Add(Uri, barier);
                            downloadPage = true;
                        }
                    }

                    if (downloadPage)
                    {
                        HtmlDocumentDictionary.Add(Uri, new HtmlWeb().Load(Uri.ToString()));
                        barier.Set();
                    }
                    else
                    {
                        WaitForPageDownload(barier);
                    }
                }
                else
                {
                    WaitForPageDownload(barier);
                }

                return HtmlDocumentDictionary[Uri];
            }
        }

        private void WaitForPageDownload(ManualResetEventSlim barier)
        {
            barier = UriBarierDictionary[Uri];
            barier.Wait();
        }
    }

}
