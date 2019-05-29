using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.Resources;
using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.Tag;
using Rupor.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? EditorId { get; set; }
        public int StatusId { get; set; }
        public DateTime? LastModify { get; set; } 

        [ForeignKey(nameof(TitleImageId))]
        public FileEntity TitleImage { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual ProfileEntity Author { get; set; }

        [ForeignKey(nameof(EditorId))]
        public virtual ProfileEntity Editor { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual AppResourceEntity Status { get; set; }

        public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<SectionEntity> Sections { get; set; }
        public virtual ICollection<FileEntity> AttachedFiles { get; set; }

    }
}
