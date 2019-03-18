using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.Account
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public IEnumerable<AuthenticationDescription> ExternalProvider { get; set; }
        public bool RememberMe { get;  set; }
    }


    public class RegisterModel
    {
        [Required(ErrorMessage = "Укажите Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
        public bool ExternLoginSucces { get; set; }

        public string OwnerId { get; set; }
        public string GivenName { get; set; }
    }

    public class ExternalLoginModel
    {
        public string Name { get; set; }
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "Введите Email!")]
        public string Email { get; set; }
        public bool ExternLoginSucces { get; set; }
    }
}