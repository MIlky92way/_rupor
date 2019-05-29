using Rupor.Public.Models.User;
using Rupor.Services.Core.Common;
using System.Collections.Generic;

namespace Rupor.Public.Areas.Cab.Models.Users
{
    public class UsersIndexViewModel
    {
        private IServiceCore service;
        public IEnumerable<RuporUser> Users { get; set; }

        public UsersIndexViewModel()
        {

        }

        public UsersIndexViewModel(IServiceCore service)
        {
            this.service = service;
            //TODO описать для юзеров
        }

    }
}