﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Rupor.Auth.Manager;
using Rupor.Domain.Entities.User;
using Rupor.Logik.Profile;
using Rupor.Public.Models.Account;
using Rupor.Tools.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Controllers
{
    public class AccountController : BaseController
    {
        
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
        }


        public ActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var authUser = UserManager.Find(model.Email, model.Password);
                if (authUser == null)
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
                else
                {
                    ClaimsIdentity clIdent = UserManager.CreateIdentity(authUser, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe, AllowRefresh = true }, clIdent);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }



        public ActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            var user = new UserEntity();
            if (ModelState.IsValid)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                user.LockoutEnabled = true;
                user.LockoutEndDateUtc = DateTime.Now.AddHours(7);

                IdentityResult identResult = UserManager.Create(user, model.Password);



                if (identResult.Succeeded)
                {
                    #region profile

                    var profile = new ProfileEntity();

                    profile.Email = user.Email;
                    profile.OwnerId = user.Id;
                    profile.LastAuth = DateTime.Now;
                    profile.GivenName = model.GivenName;

                    var result = ProfileService.Edit(profile);

                    if (result != null && result.Id > 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "При сохранении профиля произошли ошибки!");
                    }
                    #endregion
                    
                }
                else
                {
                    AddErrorsFromResult(identResult);
                }
            }


            return View(model);
        }


        [NonAction]
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}