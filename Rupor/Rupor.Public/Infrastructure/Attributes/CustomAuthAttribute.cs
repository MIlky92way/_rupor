using System.Web.Mvc;

namespace Rupor.Public.Infrastructure.Attributes
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {

        public CustomAuthAttribute(string[] roles)
        {
            Roles = string.Join(",", roles);
        }        
    }
}