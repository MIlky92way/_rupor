using Rupor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.Reference
{
    public class ReferenceSectionEntity : BaseEntity
    {
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(280)]
        public string Alias { get; set; }
    }
}
