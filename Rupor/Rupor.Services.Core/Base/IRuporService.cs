using Rupor.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Base
{
    public interface IRuporService<TEntity>
        where TEntity:class
    {
        TEntity this[int id] { get; }

        TEntity Edit(TEntity editedInstance);

        TEntity Find(int id);        
    }
}
