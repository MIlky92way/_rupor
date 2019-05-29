using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.User;
using Rupor.Logik.File;
using Rupor.Public.Infrastructure.FileTools;
using Rupor.Services.Core.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Infrastructure.ProfileTools
{
    public class ProfileWeb
    {
        private readonly ProfileEntity Profile;
        private ImageTools ImageTools;
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }        
        public IEnumerable<RoleEntity> Roles { get; set; }
        public int MinImageId { get; set; }
        public int OriginalImageId { get; set; }
        private readonly IUserProfileService<ProfileEntity> ProfileService;
        public UserEntity IdentityUser { get; private set; }
        private string path;
        public bool IsGuest { get; set; }
        public ProfileWeb()
        {
            GivenName = "guest";
            IsGuest = true;
        }

        public ProfileWeb(IUserProfileService<ProfileEntity> profileService, UserEntity user, ImageTools imgTools)
        {
            ProfileService = profileService ?? throw new ArgumentNullException("profileService");
            Profile = profileService.Get(user.Id) ?? throw new NullReferenceException("profile");
            ImageTools = imgTools ?? throw new ArgumentNullException("imgTools");
            IdentityUser = user ?? throw new ArgumentNullException("user");
            GivenName = Profile.GivenName;
            FamilyName = Profile.FamilyName;
            BirthDate = Profile.BirthDate;
            Id = Profile.Id;
            MinImageId = Profile.MinPictureId.GetValueOrDefault();
            OriginalImageId = Profile.OriginalPictureId.GetValueOrDefault();
            
        }

        public void UpdateLastAuth()
        {
            ProfileService.UpdateLastAuth(IdentityUser.Id);
        }

        /// <summary>
        /// Миниатюрное изображение в виде выходного потока, используется только для отдачи клиенту.
        /// </summary>
        /// <returns></returns>
        public FileStreamResult GetMinImage()
        {
            FileStreamResult result = null;
            var contentType = string.Empty;

            if (Profile.MinPicture == null)
            {
                var defaultPic = GetDefaultPhoto();
                contentType = defaultPic.ContentType;
                path = defaultPic.FileName;
            }
            else
            {
                path = ImageTools.Server.MapPath($"{ImageTools._PicPathProfile}/{Profile.MinPicture.Name}");
                contentType = Profile.MinPicture.ContentType;
            }

            result = new FileStreamResult(ImageTools.GetFile(path), contentType);

            return result;
        }
        /// <summary>
        /// Оригинальное изображение в виде выходного потока, используется только для отдачи клиенту.
        /// </summary>
        /// <returns></returns>
        public FileStreamResult GetOriginalImage()
        {
            FileStreamResult result = null;
            var contentType = string.Empty;

            if (Profile.OriginalPicture == null)
            {                
                var defaultPic = GetDefaultPhoto();
                contentType = defaultPic.ContentType;
                path = defaultPic.FileName;
            }
            else
            {
                path = ImageTools.Server.MapPath($"{ImageTools._PicPathProfile}/{Profile.OriginalPicture.Name}");
                contentType = Profile.OriginalPicture.ContentType;
            }

            result = new FileStreamResult(ImageTools.GetFile(path), contentType);

            return result;
        }

        

        private DefaultPicture GetDefaultPhoto()
        {
            DefaultPicture defaultPic = new DefaultPicture();

            var defaultFiles = ImageTools.FileService
                .Get(f => f.IsDefault &&
            f.FileType == FileType.Image &&
            f.FileArea == FileArea.Profile);

            var file = defaultFiles
                .FirstOrDefault();

            var path = ImageTools.Server
                .MapPath($"{ImageTools._PicPathDefaultProfile}/{file.Name}");

            if (file == null)
            {
                throw new NullReferenceException("Default profile picture is empty!");
            }

            defaultPic.FileName = path;
            defaultPic.ContentType = file.ContentType;

            return defaultPic;
        }

        public override string ToString()
        {
            return $"{GivenName} {FamilyName}";
        }

        private class DefaultPicture
        {
            public string ContentType { get; set; }
            public string FileName { get; set; }
        }
    }
}