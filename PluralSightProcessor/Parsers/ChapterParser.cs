﻿namespace PluralSightProcessor.Parsers
{
    using System;
    using System.Collections.Generic;
    using HtmlAgilityPack;
    using PluralSightProcessor.Domain;
    using System.Threading.Tasks;

    class ChapterParser
    {
        string ChapterNodeXPath = TrainingVideosProcessor.Config.ChapterNodeXPath;
        string ChapterTitleXPath = TrainingVideosProcessor.Config.ChapterTitleXPath;
        string VideosXPath = TrainingVideosProcessor.Config.VideosXPath;
        string VideoNameXPath = TrainingVideosProcessor.Config.VideoNameXPath;

        public List<Chapter> Parse(Uri courseUrl)
        {
            if (courseUrl == null)
            {
                throw new ArgumentNullException("courseUrl");
            }
            List<Chapter> result = new List<Chapter>();

            HtmlDocument webPage = new DocumentFactory(courseUrl).GetDocument as HtmlDocument;

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
                    chapter.Number = chapterNumber++;

                    ParseVideo(chapterNode, chapter);

                    result.Add(chapter);
                }
            }

            return result;
        }

        public static Task<List<Chapter>> ParseAsync(Uri courseUrl)
        {
            Task<List<Chapter>> task = new Task<List<Chapter>>(() => new ChapterParser().Parse(courseUrl));
            task.Start();

            return task;
        }

        private void ParseVideo(HtmlNode chapterNode, Chapter chapter)
        {
            string videoXPath = String.Format(VideosXPath, chapterNode.Attributes["id"].Value);
            HtmlNodeCollection videoNodesForChapter = chapterNode.SelectNodes(videoXPath);

            foreach (var videoNode in videoNodesForChapter)
            {
                Video video = new Video();
                video.Name = videoNode.SelectSingleNode(VideoNameXPath).InnerText.Replace("\\r\\n", "").Trim();

                chapter.Videos.Add(video);
            }
        }
    }
}
