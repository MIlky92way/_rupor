using Rupor.Domain.Entities.User;
using Rupor.Logik.Feed;
using Rupor.Logik.File;
using Rupor.Logik.Profile;
using Rupor.Logik.Section;
using Rupor.Services.Core.Base;
using Rupor.Services.Core.Common;
using Rupor.Services.Core.Feed;
using Rupor.Services.Core.File;
using Rupor.Services.Core.Profile;
using Rupor.Services.Core.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.Common
{
    public class ServiceCore : BaseService, IServiceCore
    {
        public IFeedChannelService CahnnelService => GetInstance<FeedChannelService>();

        public IUserProfileService<ProfileEntity> ProfileService => GetInstance<UserProfileService>();

        public IFileService FileService => GetInstance<FileService>();

        public ISectionService SectionService => GetInstance<SectionService>();
        
    }
}
