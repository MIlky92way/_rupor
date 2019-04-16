using Rupor.Services.Core.Base;
using Rupor.Services.Core.Base.Models;
using Rupor.Services.Core.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Profile
{
    public interface IUserProfileService<TEntity> : IRuporService<TEntity>
        where TEntity : class
    {
        TEntity this[string email] { get; }

        IEnumerable<TEntity> Get(BaseModel model, Expression<Func<bool, TEntity>> predicate);

        void UpdatePictue(PictureModel model);
    }
}
