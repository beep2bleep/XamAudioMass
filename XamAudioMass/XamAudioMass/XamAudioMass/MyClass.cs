using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XamAudioMass
{
	public class AudioFeedRetriever
	{
        public List<FeedItem> AudioFeeds;

		public AudioFeedRetriever()
		{
            AudioFeeds = FeedService.GetFeedItems("https://feeds.feedburner.com/usccb/zhqs");
        }

        internal static class FeedService
        {
            internal static List<FeedItem> GetFeedItems(string url)
            {
                List<FeedItem> feedItemsList = new List<FeedItem>();

                try
                {
                    System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    Stream stream = webResponse.GetResponseStream();
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(stream);
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
                    nsmgr.AddNamespace("dc", xmlDocument.DocumentElement.GetNamespaceOfPrefix("dc"));
                    nsmgr.AddNamespace("content", xmlDocument.DocumentElement.GetNamespaceOfPrefix("content"));
                    XmlNodeList itemNodes = xmlDocument.SelectNodes("rss/channel/item");

                    for (int i = 0; i < itemNodes.Count; i++)
                    {
                        FeedItem feedItem = new FeedItem();

                        if (itemNodes[i].SelectSingleNode("title") != null)
                        {
                            feedItem.Title = itemNodes[i].SelectSingleNode("title").InnerText;
                        }
                        if (itemNodes[i].SelectSingleNode("link") != null)
                        {
                            feedItem.Link = itemNodes[i].SelectSingleNode("link").InnerText;
                        }
                        if (itemNodes[i].SelectSingleNode("pubDate") != null)
                        {
                            //feedItem.PubDate = Convert.ToDateTime(itemNodes[i].SelectSingleNode("pubDate").InnerText);
                        }
                        if (itemNodes[i].SelectSingleNode("dc:creator", nsmgr) != null)
                        {
                            feedItem.Creator = itemNodes[i].SelectSingleNode("dc:creator", nsmgr).InnerText;
                        }
                        if (itemNodes[i].SelectSingleNode("category") != null)
                        {
                            feedItem.Category = itemNodes[i].SelectSingleNode("category").InnerText;
                        }
                        if (itemNodes[i].SelectSingleNode("description") != null)
                        {
                            feedItem.Description = itemNodes[i].SelectSingleNode("description").InnerText;
                        }
                        if (itemNodes[i].SelectSingleNode("content:encoded", nsmgr) != null)
                        {
                            feedItem.Content = itemNodes[i].SelectSingleNode("content:encoded", nsmgr).InnerText;
                        }
                        else
                        {
                            feedItem.Content = feedItem.Description;
                        }
                        feedItemsList.Add(feedItem);
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                return feedItemsList;
            }
        }

        public class FeedItem
        {
            public FeedItem()
            {
            }

            public string Title { get; set; }
            public string Link { get; set; }
            public DateTime PubDate { get; set; }
            public string Creator { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string Content { get; set; }
        }


    }



    
}

