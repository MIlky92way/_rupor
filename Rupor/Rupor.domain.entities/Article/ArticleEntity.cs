using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
