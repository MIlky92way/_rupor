using Rupor.Feed.Core.Types;
using Rupor.Tools.Extend;
using System.Text;
using System.Xml.Linq;

namespace Rupor.Feed.Core.Parsers
{
    internal class DocInitializer : IDocInitializer
    {
        private XDocument xdoc;

        public FeedType FeedType { get; private set; }

        public XDocument Initial(string content)
        {
            var bytes = Encoding.Default.GetBytes(content);
            content = Encoding.UTF8.GetString(bytes);
            content = Clean(content);
            xdoc = XDocument.Parse(content);
            var encoding = GetEncoding(xdoc);

            if (encoding != Encoding.UTF8)
            {
                content = encoding.GetString(bytes);
                content = Clean(content);
            }
            FeedType = DetermineFeedType();

            return xdoc;
        }

        private FeedType DetermineFeedType()
        {
            FeedType feedType = FeedType.Default;
            var rootName = xdoc.Root.Name.LocalName;

            if (rootName.EqualsIgnoreCase("feed"))
                feedType = FeedType.Atom;

            if (rootName.EqualsIgnoreCase("rss"))
            {
                var ver = xdoc.Root.Attribute("version").Value;
                if (ver.EqualsIgnoreCase("2.0"))
                    feedType = FeedType.Default;
            }

            return feedType;
        }

        private Encoding GetEncoding(XDocument xdoc)
        {
            Encoding encoding = Encoding.UTF8;
            var encStr = xdoc.Document.Declaration?.Encoding;
            try
            {
                if (!string.IsNullOrEmpty(encStr))
                    encoding = Encoding.GetEncoding(encStr);
            }
            catch { }

            return encoding;
        }

        private string Clean(string content)
        {
            content = content.Replace(((char)28).ToString(), string.Empty);
            content = content.Replace(((char)65279).ToString(), string.Empty);
            return content.Trim();
        }
    }
}
