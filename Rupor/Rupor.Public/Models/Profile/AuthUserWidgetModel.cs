using Rupor.Domain.Entities.User;
using Rupor.Logik.Profile;
using Rupor.Public.Infrastructure.ProfileTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.Profile
{
    public class AuthUserWidgetModel
    {
        public bool IsAuth { get; set; }
        public string Name { get; set; }
        //public string ImagePath { get; set; }

        public AuthUserWidgetModel()
        {

        }

        public AuthUserWidgetModel(bool isAuth, ProfileWeb profile = null)
        {

            if (isAuth && profile == null)
            {
                //TODO Log 
                throw new ArgumentNullException("profile");
            }
            else if(isAuth)
            {
                Name = profile.ToString();
            }
            
            IsAuth = isAuth;
           
        }
    }
}