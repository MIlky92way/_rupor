using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.User
{
    [Table("ProfileSettings")]
    public class ProfileSettingsEntity:BaseEntity
    {
        public ProfileSettingsEntity()
        {

        }
        public int? ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual ProfileEntity Profile { get; set; }

        public virtual ICollection<SectionEntity> SubscribeOnSections { get; set; }
        public virtual ICollection<ProfileEntity> SubscribeOnAuthors { get; set; }        
    }
}
