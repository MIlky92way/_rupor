using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.domain.Context
{
    public partial class RuporDbContext:IdentityDbContext<UserEntity>
    {
        public RuporDbContext()
        {

        }

    }
}
