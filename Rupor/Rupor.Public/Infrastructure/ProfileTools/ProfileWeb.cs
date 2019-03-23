using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.User;
using Rupor.Logik.File;
using Rupor.Public.Infrastructure.FileTools;
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
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public IEnumerable<RoleEntity> Roles { get; set; }
             
        private string path;

        public ProfileWeb(ProfileEntity profile, ImageTools imgTools)
        {
            Profile = profile ?? throw new ArgumentNullException("profile");
            ImageTools = imgTools ?? throw new ArgumentNullException("imgTools");
            
            GivenName = Profile.GivenName;
            FamilyName = Profile.FamilyName;
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