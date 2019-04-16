using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Base
{
    public interface IRuporService<TEntity>
        where TEntity : class
    {
        TEntity this[int id] { get; }
        IEnumerable<TEntity> Get();
        TEntity Edit(TEntity editedInstance);
        void Remove(TEntity entry);
    }
}
