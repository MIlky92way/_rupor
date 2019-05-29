using Rupor.Domain.Context;
using System;

namespace Rupor.Logik.Base
{
    public class EFBaseService : IDisposable
    {
        protected RuporDbContext DbContext { get; private set; }

        public EFBaseService()
        {
            DbContext = CreateContext();
        }


        private RuporDbContext CreateContext()
        {
            return new RuporDbContext();
        }

        public void Dispose()
        {

        }
    }
}
