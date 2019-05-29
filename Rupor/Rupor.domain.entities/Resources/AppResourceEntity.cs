using Rupor.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rupor.Domain.Entities.Resources
{
    [Table("AppResource")]
    public class AppResourceEntity : BaseEntity
    {
       
        public int Key { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual AppResourceSectionEntity Section { get; set; }
    }
}
