using Rupor.Domain.Entities.User;
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Public.Models.Dto;
using Rupor.Public.Models.User;
using Rupor.Services.Core.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.Profile
{
    public class ProfileIndexViewModel
    {
        private IServiceCore service;
        private ProfileEntity user;
        private ProfileWeb current;
        public string FullName { get; set; }
        public int? FeedSettingsId { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<CategoryDto> FavoriteCategories { get; set; }
        public IEnumerable<RuporUser> FavoriteAuthors { get; set; }


        public ProfileIndexViewModel()
        {

        }

        public ProfileIndexViewModel(IServiceCore service, ProfileWeb current, int userId = 0)
        {
            this.service = service;
            this.current = current;
            user = null;

            if (userId == 0)
            {
                user = service.ProfileService[current.Id];
            }
            else
            {
                user = service.ProfileService[userId];
            }

            FullName = $"{user.GivenName} {user.FamilyName}";
            FeedSettingsId = user.ProfileSettingsId;
            var settings = user.ProfileSettings;
            FavoriteCategories = GetMyCategories(service);
        }

        public IEnumerable<CategoryDto> GetMyCategories(IServiceCore service)
        {
            IEnumerable<CategoryDto> categories = null;
            categories = service.ProfileSettings
                .Settings
                .FirstOrDefault(s => s.ProfileId == current.Id)?
                .SubscribeOnSections
                .Select(s => new CategoryDto
                {
                    Id = s.Id,
                    ImageId = s.ImageId,
                    Name = s.Name
                }).ToList() ?? new List<CategoryDto>();

            return categories;
        }
    }

    public class UserInfoViewModel
    {
        private IServiceCore service;
        public string FullName { get; set; }
        public int CountComments { get; set; }
        public int CountSubscribes { get; set; }
        public int OriginalImageId { get; set; }
        public int MinImageId { get; set; }
        public bool IsCurrent { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public bool? IsDefaultPicture { get; set; }
        public UserInfoViewModel()
        {

        }
        public UserInfoViewModel(IServiceCore service, ProfileWeb current, int userId = 0)
        {
            this.service = service;
            ProfileEntity user = null;

            IsDefaultPicture = service.FileService[current.OriginalImageId]?.IsDefault;

            if (userId == 0)
            {
                IsCurrent = true;
                user = service.ProfileService[current.Id];
            }
            else
            {
                user = service.ProfileService[userId];
                IsCurrent = user.OwnerId == current.IdentityUser.Id;
            }

            FullName = $"{user.GivenName} {user.FamilyName}";
            OriginalImageId = user.OriginalPictureId.GetValueOrDefault();
            MinImageId = user.MinPictureId.GetValueOrDefault();
        }
    }
}