using Rupor.Domain.Entities.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Areas.Cab.Models
{
    public class ProfileSettingsModel
    {
        public HttpPostedFileBase File { get; set; }
       
        public FileArea FileArea { get; private set; }

        public FileType FileType { get; set; }

        public bool IsEmptyDefaultImage { get; set; }
        public ProfileSettingsModel()
        {
            FileArea = FileArea.Profile;            
        }

    }
}