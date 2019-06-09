using Rupor.Domain.Entities.Resources;
using Rupor.Domain.Entities.User;
using Rupor.Logik.App;
using Rupor.Logik.Article;
using Rupor.Logik.Feed;
using Rupor.Logik.File;
using Rupor.Logik.Profile;
using Rupor.Logik.Section;
using Rupor.Services.Core.App;
using Rupor.Services.Core.Article;
using Rupor.Services.Core.Base;
using Rupor.Services.Core.Common;
using Rupor.Services.Core.Feed;
using Rupor.Services.Core.File;
using Rupor.Services.Core.Profile;
using Rupor.Services.Core.Section;

namespace Rupor.Logik.Common
{
    public class ServiceCore : BaseService, IServiceCore
    {
        public IFeedChannelService CahnnelService => GetInstance<FeedChannelService>();

        public IUserProfileService<ProfileEntity> ProfileService => GetInstance<UserProfileService>();

        public IFileService FileService => GetInstance<FileService>();

        public ISectionService SectionService => GetInstance<SectionService>();

        public ISectionSettingsService SectionSettingsService => GetInstance<SectionSettingsService>();

        public IProfileSettngs ProfileSettings => GetInstance<ProfileSettingsService>();

        public IAppResourceService AppResourceService => throw new System.NotImplementedException();

        public IAppResourceSectionService AppResourceSectionService => GetInstance<AppResourceSectionService>();

        public IArticleService ArticleService => GetInstance<ArticleService>();
    }
}
