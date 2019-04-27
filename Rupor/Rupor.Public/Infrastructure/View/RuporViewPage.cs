using Microsoft.AspNet.Identity.Owin;
using Rupor.Auth.Manager;
using Rupor.Domain.Entities.User;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Infrastructure.View
{
    public abstract class RuporViewPage<TModel> : WebViewPage
    {
        public UserEntity CurrentUser => GetCurrentUserAsync().Result;

        private async Task<UserEntity> GetCurrentUserAsync()
        {
            UserEntity current = null;
            if (User.Identity.IsAuthenticated)
            {
                AppUserManager um = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

                current = await um.FindByNameAsync(User.Identity.Name);
            }

            return current;
        }


    }
}