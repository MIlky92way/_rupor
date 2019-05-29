using Rupor.Public.Infrastructure.FileTools;
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Services.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.UserPhoto
{
    public class CropModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int OrigPicId { get; set; }
        public int UserId { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string UserFullName { get; set; }
        public CropModel()
        {

        }

        public CropModel(ProfileWeb current)
        {
            OrigPicId = current.OriginalImageId;
            UserFullName = current.ToString();
            UserId = current.Id;
        }
    }
}