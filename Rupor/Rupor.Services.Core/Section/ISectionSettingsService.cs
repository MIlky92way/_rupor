using Rupor.Domain.Entities.Section;
using Rupor.Services.Core.Section.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Section
{
    public interface ISectionSettingsService 
    {
        SectionSettingsModel Settings { get; }

        IList<SectionEntity> DefaultSections { get; }
        SectionEntity GetDefaultSection(int id);
        void Edit(SectionSettingsModel model);
        void RemoveDefaultSettings();
        void RemoveDefaultSection(int id);
    }
}
