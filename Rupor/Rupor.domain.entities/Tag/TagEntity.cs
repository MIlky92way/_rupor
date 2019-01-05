﻿using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.Tag
{
    [Table("Tag")]
    public class TagEntity : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        public ICollection<ArticleEntity> Articles { get; set; }

    }
}
