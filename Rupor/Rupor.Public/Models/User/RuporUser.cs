using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.User
{
    public class RuporUser
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string UrlToImageMin { get; set; }
        public string UrlToOriginalImage { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}