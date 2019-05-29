using Rupor.Public.Infrastructure.Attributes;
using Rupor.Tools.Consts;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    [CustomAuth(roles: new[] { Role._ROOT })]
    public class InitialController : BaseController
    {
        // GET: Initial
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Initial()
        {
            return RedirectToAction("Index");
        }
    }
}