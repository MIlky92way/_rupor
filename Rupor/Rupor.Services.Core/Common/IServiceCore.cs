using Rupor.Domain.Entities.User;
using Rupor.Services.Core.App;
using Rupor.Services.Core.Base;
using Rupor.Services.Core.Feed;
using Rupor.Services.Core.File;
using Rupor.Services.Core.Profile;
using Rupor.Services.Core.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Common
{
    public interface IServiceCore
    {
        IFeedChannelService CahnnelService { get; }
        IUserProfileService<ProfileEntity> ProfileService { get; }
        IFileService FileService { get; }
        ISectionService SectionService {get;}
        ISectionSettingsService SectionSettingsService { get; }
        IProfileSettngs ProfileSettings { get; }
        IAppResourceService AppResourceService { get; }
        IAppResourceSectionService AppResourceSectionService { get; }

    }
}
