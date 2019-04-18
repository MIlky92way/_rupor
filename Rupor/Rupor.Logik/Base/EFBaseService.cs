using Rupor.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.Base
{
    internal class EFBaseService
    {
        protected RuporDbContext DbContext => CreateContext();

        public EFBaseService()
        {
            
        }


        private RuporDbContext CreateContext()
        {
            return new RuporDbContext();
        }

    }
}
