using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.Reference
{
    [Table("Reference")]
    public class ReferenceEntity
    {
        [MaxLength(255)]
        public string Key { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }

        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public ReferenceSectionEntity Section { get; set; }
    }
}
