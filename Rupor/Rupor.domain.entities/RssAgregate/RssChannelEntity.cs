using Rupor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.User;

namespace Rupor.Domain.Entities.RssAgregate
{
    [Table("RssChanel")]
    public class RssChannelEntity : BaseEntity
    {
        public RssChannelEntity()
        {
            RssFeeds = new HashSet<RssFeedEntity>();
        }

        public string TargetUrl { get; set; }

        [Obsolete("Не используется", true)]
        public string UrlToPicture { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(520)]
        public string Description { get; set; }
        
        public int? ChannelPictureId { get; set; }

        [ForeignKey("ChannelPictureId")]
        public FileEntity ChannelPicture { get; set; }

        public ICollection<RssFeedEntity> RssFeeds { get; set; }

        public virtual  ICollection<ProfileEntity> Subscribers { get; set; }

    }
}
