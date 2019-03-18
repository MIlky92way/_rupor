using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.RssAgregate
{
    [Table("RssFeed")]
    public class RssFeedEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public int CahnnelId { get; set; }
        public RssChannelEntity Cahnnel { get; set; }
        public ICollection<TagEntity> Tags { get;set; }

    }
}
