using System;
using HtmlAgilityPack;
using System.Threading;
using System.Collections.Generic;
using System.Xml;

namespace PluralSightProcessor
{
    public class DocumentFactory : ILibraryDocumentFactory
    {
        private static int numConcWebRequests = 10;

        public static int NumConcWebRequests
        {
            get { return numConcWebRequests; }
            set {
                for (int i = 0; i < numConcWebRequests; i++)
                {
                    ConcurentWebRequests.WaitOne();
                }
                numConcWebRequests = value;
                ConcurentWebRequests = new Semaphore(numConcWebRequests, numConcWebRequests);
            }
        }

        protected Uri Uri { get; set; }

        private static Semaphore ConcurentWebRequests = new Semaphore(numConcWebRequests, numConcWebRequests);

        private static Dictionary<Uri, ManualResetEventSlim> UriBarierDictionary = new Dictionary<Uri, ManualResetEventSlim>();

        private static Object barierDicSync = new Object();

        private static Dictionary<string, HtmlDocument> HtmlDocumentDictionary = new Dictionary<string, HtmlDocument>();
        
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
                        int numTry = 0;
                        HtmlDocument doc = null;
                        ConcurentWebRequests.WaitOne();
                        Retry:
                        try
                        {
                            
                            doc = new HtmlWeb().Load(Uri.ToString());
                            ConcurentWebRequests.Release(1);
                        }
                        catch
                        {
                            if (numTry < 5) {
                                numTry++;
                                Console.WriteLine(numTry + " : " + Uri.ToString());
                                goto Retry; 
                            }
                            throw;
                        }

                        HtmlDocumentDictionary.Add(Uri.AbsoluteUri, doc);

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

                return HtmlDocumentDictionary[Uri.AbsoluteUri];
            }
        }

        private void WaitForPageDownload(ManualResetEventSlim barier)
        {
            barier = UriBarierDictionary[Uri];
            barier.Wait();
        }
    }

}
