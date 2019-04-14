using System.Xml.Linq;
using Rupor.Feed.Core.Types;

namespace Rupor.Feed.Core
{
    internal interface IDocInitializer
    {
        XDocument Initial(string content);

        FeedType FeedType { get; } 
    }
}