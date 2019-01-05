using System;
using Rupor.Domain.Entities.Tag;
using Rupor.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rupor.Domain.Entities.Article
{
    [Table("Article")]
    public class ArticleEntity : PageEntity
    {
        [MaxLength(255)]
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
    }
}
