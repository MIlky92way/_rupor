using Rupor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rupor.Domain.Entities.File;

namespace Rupor.Domain.Entities.Section
{
    /// <summary>
    /// Общие Настройки разделов
    /// </summary>
    public class SectionSettingsEntity:BaseEntity
    {
        public int MaxAllowedSections { get; set; }
        public int MaxAllowedSectionsOnTop { get; set; }
        public int? DefaultPictureId { get; set; }

        [ForeignKey("DefaultPictureId")]
        public FileEntity DefaultPicture { get; set; }
    }
}
