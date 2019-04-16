using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Rupor.Tools.Extend;

namespace Rupor.Feed.Feeds
{
   public class BaseFeed
    {
        public string Title { get; set; }
        public string Link { get; set; }
                      
        public BaseFeed(){}

        public BaseFeed(XElement channel):this()
        {
            //TODO BaseFeed init props!
            Title = channel.GetValue("title");
            Link = channel.GetValue("link");            
        }


    }
}
