using Rupor.Services.Core.Base;
using Rupor.Services.Core.Base.Models;
using Rupor.Services.Core.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rupor.Services.Core.Profile
{
    public interface IUserProfileService<TEntity> : IRuporService<TEntity>, IEnumerable<TEntity>
        where TEntity : class
    {
        TEntity this[string email] { get; }
        /// <summary>
        /// Создает новый профиль, используется только при регитсрраци нового пользователя
        /// </summary>
        /// <param name="ownerId">Identity user id</param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        TEntity Create(string ownerId, string email, string userName = null);
        IEnumerable<TEntity> Get(BaseModel model, Expression<Func<bool, TEntity>> predicate);
        TEntity Get(string ownerId);
        void UpdateOriginalPicture(int profileId, int pictureId);
        void UpdateMiniaturePicture(int profileId, int pictureId);
        void UpdateLastAuth(string owner);
        void UpdateOwner(int id, string ownerId);
        void UpdateOwner(string oldOwnerId, string newOwnerId);        
    }
}
