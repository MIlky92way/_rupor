using Rupor.Logik.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.Base
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
