using Rupor.Domain.Entities.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Section.Models
{
    /// <summary>
    /// Область для обработки разделов, если пишется дефолтный раздел то базовых натсроек  не будет затронут.
    /// </summary>
    public enum SectionSettingArea
    {
        Settings,
        DefaultSections
    }
    /// <summary>
    /// Агрегирует данные. Дефолтный раздел и общие настройки.
    /// </summary>
    public class SectionSettingsModel : SectionSettingsEntity
    {
        public SectionSettingArea SettingsArea { get; set; }

        public IEnumerable<SectionEntity> DefaultSections { get; set; }

        public int SectionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
        /// <summary>
        /// Является стандартным (исп. когда нет других  разделов созданных модератором.) 
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Отображение на шапке сайта
        /// </summary>
        public bool OnTop { get; set; }

    }
}
