using System;
using System.Net;

namespace Rupor.Feed.Utility
{
    public static class UrlUtility
    {        
        private const string HEADER_ACCEPT_VAL = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";        
        private const string HEADER_USER_AGENT_VAL = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";


        public static string HttpGetString(string url)
        {
            var result = string.Empty;

            using (WebClient client = new WebClient())
            {

                client.Headers.Add(HttpRequestHeader.Accept, HEADER_ACCEPT_VAL);
                client.Headers.Add(HttpRequestHeader.UserAgent, HEADER_USER_AGENT_VAL);
                url = GetCorrectUrl(url);
                try
                {
                    result = client.DownloadString(url);
                }
                catch (Exception e)
                {
                    
                }
                
            }

            return result;
        }


        public static string GetCorrectUrl(string url)
        {
            return new UriBuilder(url).ToString();
        }
    }
}
