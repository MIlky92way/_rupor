using Rupor.Domain.Entities.User;
using Rupor.Logik.Profile;
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Services.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Helpers
{
    public static class UserNameHelper
    {
        /// <summary>
        /// Получает имя текущего пользователя
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="user"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns></returns>
        public static string GetUserName(this HtmlHelper helper, IPrincipal user)
        {            
            IUserProfileService<ProfileEntity> profsrv = null;

            if (user.Identity.IsAuthenticated)
            {
                profsrv = new UserProfileService();
                var profile = profsrv[user.Identity.Name];

                return profile.GivenName ?? throw new NullReferenceException("User is not found!");
            }
            else
            {
                throw new NotSupportedException("User is not authentication!");
            }
        }
    }
}