using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rupor.Feed.Core.Readers;
using Rupor.Feed.Utility;
using System.Collections.Generic;

namespace Rupor.UnitTest
{
    [TestClass]
    public class FeedParserTest
    {
        [TestMethod]
        public void HttpGET()
        {
            var urls = new string[] { "lenta.ru/rss/news", "www.aif.ru/rss/politics.php", "static.feed.rbc.ru/rbc/logical/footer/news.rss", "infokam.su/rssall/" };

            IList<int> lengths = new List<int>();
            foreach (var url in urls)
            {
                var strRes = UrlUtility.HttpGetString(url);
                if (strRes.Length > 0)
                {
                    lengths.Add(strRes.Length);
                }
            }

            Assert.AreEqual(urls.Length, lengths.Count);

        }


        [TestMethod]
        public void TestParser()
        {
            var reader = new FeedReader();
            var feed = reader.Read("tass.ru/rss/v2.xml");
        }
    }
}
