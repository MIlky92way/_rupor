using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rupor.Domain.Entities.File;

namespace Rupor.Public.Areas.Cab.Models
{
    public enum SectionSettingArea
    {
        general,
        standardSections,
    }
    public class SectionsSettingsModel
    {
        public SectionsSettingsModel()
        {
            FileArea = FileArea.Section;
        }

        public int MaxAllowedSections { get; set; }
        public HttpPostedFileBase SectionImage { get; set; }
        
        public bool SectionOnTop { get; set; }
        public SectionSettingArea SectionSettingArea{get; set; }
        public FileArea FileArea { get; private set; }
    }


    public class DefaultSectionModel
    {
        public DefaultSectionModel()
        {
            IsDefault = true;
        }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public bool OnTop { get; set; }
    }
}