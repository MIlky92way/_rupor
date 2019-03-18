using Rupor.Logik.File;
using Rupor.Public.Infrastructure.FileTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    [Authorize]
    public class UserPhotoController : BaseController
    {
        private IFileService FileService { get; }
        private ImageTools ImageTools { get; }
        public UserPhotoController(IFileService fileService)
        {
            FileService = fileService;
            ImageTools = new ImageTools(HttpContext, Server, fileService);
        }

        // GET: UserPhoto
        public FileStreamResult GetMinPicture()
        {
            FileStreamResult result = null;

            return result;
        }

        public FileStreamResult GetOriginalPicture()
        {
            FileStreamResult result = null;

            return result;
        }

        public ActionResult SavePhoto(HttpPostedFile file)
        {            
            return View();
        }


        public ActionResult DeletePhoto()
        {

            return View();
        }
    }
}