using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rupor.Domain.Entities.File;
using Rupor.Public.Models;

namespace Rupor.Public.Areas.Cab.Models
{
    public enum SectionSettingArea
    {
        general,
        standardSections,
    }
    public class SectionsSettingsModel: BaseAppSettingsModel
    {
        public SectionsSettingsModel()
        {
            FileArea = FileArea.Section;
        }

        public int DefaultPictureId { get; set; }
        public int MaxAllowedSections { get; set; }

        public HttpPostedFileBase SectionImage { get; set; }
                  
        public FileArea FileArea { get; private set; }

        public IEnumerable<DefaultSectionModel> Sections { get; set; }

    }


    public class DefaultSectionModel: BaseModel
    {
        public DefaultSectionModel()
        {
            IsDefault = true;
            IsActive = true;
        }
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public bool OnTop { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// Проверка на наличие дефолтных настроек разделов.
        /// </summary>
        public bool IsEmptySetting { get; set; }

        public int MaxAllowedSections { get; set; }
        public bool OverflowSections { get; set; }
    }
}