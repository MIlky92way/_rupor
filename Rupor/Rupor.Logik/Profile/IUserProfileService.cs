using Rupor.Logik.Base;
using Rupor.Logik.Base.Model;
using Rupor.Logik.Profile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.Profile
{
    public interface IUserProfileService<TEntity> : IRuporService<TEntity>
        where TEntity : class
    {
        TEntity this[string email] { get; }

        IEnumerable<TEntity> Get(BaseModel model,Expression<Func<bool, TEntity>> predicate);
        
        void UpdatePictue(PictureModel model);
         
    }
}
