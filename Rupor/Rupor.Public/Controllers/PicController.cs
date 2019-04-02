using System;
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
        public FileStreamResult GetDefaultProfileImage()
        {
            return ImageTools.GetDefaultImage(FileArea.Profile);
        }
    }
}