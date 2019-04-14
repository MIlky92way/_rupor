﻿using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.RssAgregate;

namespace Rupor.Domain.Entities.Section
{
    [Table("Section")]
    public class SectionEntity : BaseEntity
    {

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
        /// <summary>
        /// Является стандартным (исп. когда нет других  разделов созданных модератором.) 
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Отображение на шапке сайта
        /// </summary>
        public bool OnTop { get; set; }
        public int? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public FileEntity Image { get; set; }

        public virtual ICollection<ArticleEntity> Articles { get; set; }
        public virtual  ICollection<RssFeedEntity> RssFeeds { get; set; }

    }
}