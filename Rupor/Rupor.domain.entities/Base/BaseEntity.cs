using System;
using System.ComponentModel.DataAnnotations;

namespace Rupor.Domain.Entities.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreate = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
    }

    public class PageEntity : BaseEntity
    {
        [MaxLength(500)]
        public string MetaKeywords { get; set; }
        [MaxLength(500)]
        public string MetaDescription { get; set; }
        [MaxLength(500)]
        public string MetaTitle { get; set; }                
    }
}
