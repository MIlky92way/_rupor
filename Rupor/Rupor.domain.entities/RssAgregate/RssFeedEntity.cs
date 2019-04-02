using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rupor.Domain.Entities.Article;

namespace Rupor.Domain.Entities.RssAgregate
{
    [Table("RssFeed")]
    public class RssFeedEntity : BaseEntity
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(1024)]
        public string Content { get; set; }
        public string Image { get; set; }
        [MaxLength(75)]
        public string Category { get; set; }
        public int? ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public RssChannelEntity Channel { get; set; }
        public ICollection<TagEntity> Tags { get;set; }   
    }
}
