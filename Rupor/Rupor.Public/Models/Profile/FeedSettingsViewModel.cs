using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.User;
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Services.Core.Common;
using System.Collections.Generic;
using System.Linq;

namespace Rupor.Public.Models.Profile
{
    public class FeedSettingsModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }

        public FeedSettingsModel()
        {

        }

        public FeedSettingsModel(int id, int profileId)
        {
            ProfileId = profileId;
            Id = id;

        }
    }

    public class SubscribeSectionModel : FeedSettingsModel
    {
        public IEnumerable<SectionEntity> Sections { get; set; }

        public IList<int> SelectedSectionIds { get; set; }

        public SubscribeSectionModel()
        {

        }

        public SubscribeSectionModel(int profileId)
        {
            ProfileId = profileId;
        }

        public SubscribeSectionModel(int? id, IServiceCore service, ProfileWeb current)
        {
            ProfileSettingsEntity settings = null;

            if (id.HasValue && id > 0)
                settings = service.ProfileSettings[id.Value, current.Id];
            else
                settings = service
                    .ProfileSettings
                    .Settings
                    .FirstOrDefault(s => s.ProfileId == current.Id);

            Id = settings?.Id ?? 0;
            ProfileId = settings?.ProfileId.Value ?? current.Id;
            SelectedSectionIds = settings?
                .SubscribeOnSections
                .Select(s => s.Id).ToList() ?? new List<int>();

            var query = service.SectionService
                 .Where(s => s.IsActive && !s.IsDefault && !s.IsDelete);

            Sections = query?.Count() > 0
                 ?
                    query.ToList()
                 :
                      service.SectionService
                     .GetDefaults().ToList();
        }
    }
}