using System;
using Rupor.Domain.Entities.Tag;
using Rupor.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.User;

namespace Rupor.Domain.Entities.Article
{
    [Table("Article")]
    public class ArticleEntity : PageEntity
    {
        [MaxLength(120)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public int TitleImageId { get; set; }        

        public int AuthorId { get; set; }

        [ForeignKey("TitleImageId")]
        public FileEntity TitleImage { get; set; }

        [ForeignKey("AuthorId")]
        public ProfileEntity Author { get; set; }

        public ICollection<TagEntity> Tags { get; set; }
        public ICollection<SectionEntity> Sections { get; set; }
        public ICollection<FileEntity> AttachedFiles { get; set; }
                
    }
}
