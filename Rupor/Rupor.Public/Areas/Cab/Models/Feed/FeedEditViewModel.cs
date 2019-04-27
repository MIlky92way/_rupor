using Rupor.Domain.Entities.RssAgregate;
using System.Collections.Generic;

namespace Rupor.Public.Areas.Cab.Models.Feed
{
    public class FeedEditViewModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string TargetUrl { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public int CountFeeds { get; set; }
        public ICollection<string> Categories { get; set; }
        public bool SuccessSave { get; set; }

        public FeedEditViewModel()
        {
            Categories = new HashSet<string>();            
        }

        public FeedEditViewModel(FeedChannelEntity entity)
        {
            Name = entity.Name;
            TargetUrl = entity.TargetUrl;
            Description = entity.Description;
            ImageId = entity.ChannelPictureId;
            CountFeeds = entity.CountFeeds;
        }

        public FeedChannelEntity MapFrom()
        {
            FeedChannelEntity entity = new FeedChannelEntity();
            entity.Name = Name;
            entity.TargetUrl = TargetUrl;
            entity.ChannelPictureId = ImageId;
            entity.Description = Description;
            entity.CountFeeds = CountFeeds;
            return entity;
        }

        public void InitializerDefaulValues()
        {
            CountFeeds = 1;
        }

    }
}