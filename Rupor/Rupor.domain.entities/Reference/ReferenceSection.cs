
using System;
using Rupor.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rupor.Domain.Entities.Reference
{
    [Table("ReferenceSection")]
    public class ReferenceSectionEntity : BaseEntity
    {
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(280)]
        public string Alias { get; set; }
    }
}
