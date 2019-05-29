using Rupor.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rupor.Domain.Entities.Resources
{
    [Table("AppResourceSection")]
    public class AppResourceSectionEntity : BaseEntity
    {
        public AppResourceSectionEntity()
        {
            Resources = new HashSet<AppResourceEntity>();
        }

        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Значение из Enum
        /// </summary>
        public SectionResource Key { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }   
        
        public virtual ICollection<AppResourceEntity > Resources { get; set; }
    }
}
