using Rupor.Domain.Entities.User;
using Rupor.Services.Core.Profile.Models;
using System.Collections.Generic;

namespace Rupor.Services.Core.Profile
{
    public interface IProfileSettngs
    {
        ProfileSettingsEntity this[int id,int profileId] { get; }       
        ProfileSettingsEntity Create(int profileId);

        void SubscribeOnSections(int profileId, IList<int> sectionIds, int id = 0);
        void SubscribeOnAuthors(int profileId, IList<int> authorIds, int id = 0);
        void UnSubscribeOnSections(int id, int profileId, IList<int> sectionIds);
        void UnSubscribeOnAuthors(int id, int profileId,IList<int> authorIds);

        IEnumerable<ProfileSettingsEntity> Settings { get; }
    }
}
