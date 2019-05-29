using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Services.Core.Common;
using System;

namespace Rupor.Public.Models.Profile
{
    public class InfoViewModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string SocialLinkVk { get; set; }
        public string SocialLinkInst { get; set; }
        public string SocialLinkFb { get; set; }
        public string SocialLinkTwitter { get; set; }
        public int Id { get; set; }
        public int? MinPictureId { get; set; }
        public int? OriginalPictureId { get; set; }
        public string Email { get; set; }

        public InfoViewModel()
        {

        }

        public InfoViewModel(IServiceCore service, ProfileWeb current)
        {
            FamilyName = current.FamilyName;
            GivenName = current.GivenName;
            BirthDate = current.BirthDate;
            SocialLinkFb = service.ProfileService[current.Id]?.SocialFb;
            SocialLinkInst = service.ProfileService[current.Id]?.SocialInstagramm;
            SocialLinkVk = service.ProfileService[current.Id]?.SocialVk;
            SocialLinkTwitter = service.ProfileService[current.Id]?.SocialTwitter;
            OriginalPictureId = service.ProfileService[current.Id]?.OriginalPictureId;
            MinPictureId = service.ProfileService[current.Id]?.MinPictureId;
            Id = current.Id;
        }
    }
}