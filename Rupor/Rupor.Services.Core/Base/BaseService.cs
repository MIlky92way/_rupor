using System;

namespace Rupor.Services.Core.Base
{
    public class BaseService
    {
        protected virtual TService GetInstance<TService>()
            where TService : class, new() => Activator.CreateInstance(typeof(TService)) as TService;
        
    }
}
