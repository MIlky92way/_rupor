﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rupor.Domain.Entities.File;

namespace Rupor.Public.Controllers
{
    public class PicController : BaseController
    {
        // GET: Pic
        public ActionResult Index()
        {
            return View();
        }


        //Pic/GetDefaultProfileImage
        public FileStreamResult GetProfileImage()
        {
            return ImageTools.GetDefaultImage(FileArea.Profile);
        }

        public FileStreamResult GetSectionImage(int id = 0)
        {
            return ImageTools.GetDefaultImage(FileArea.Section, id);
        }
    }
}