using Rupor.Domain.Entities.User;
using Rupor.Logik.Base;
using Rupor.Services.Core.Base.Models;
using Rupor.Services.Core.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace Rupor.Logik.Profile
{

    public class UserProfileService : EFBaseService, IUserProfileService<ProfileEntity>
    {

        private DbSet<ProfileEntity> Users { get; set; }

        public ProfileEntity this[int id] => Get(id);

        public UserProfileService()
        {
            Users = DbContext.Set<ProfileEntity>();
        }

        public ProfileEntity this[string email]
        {
            get
            {
                ProfileEntity entity = null;

                if (!string.IsNullOrEmpty(email) || !string.IsNullOrWhiteSpace(email))
                {
                    entity = Users
                    .FirstOrDefault(x => x.Email == email);
                }

                return entity;
            }
        }

        public ProfileEntity Edit(ProfileEntity model)
        {
            ProfileEntity entity = null;

            if (model.Id > 0)
            {
                entity = Users
                    .Include(x => x.Owner)
                    .FirstOrDefault(x => x.Id == model.Id);

                DbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entity = new ProfileEntity();
                Users.Add(entity);
            }

            entity.GivenName = model.GivenName;
            entity.FamilyName = model.FamilyName;
            entity.Patronymic = model.Patronymic;
            entity.BirthDate = model.BirthDate;
            entity.FileNameOriginal = model.FileNameOriginal;
            entity.FileNameMin = model.FileNameMin;

            entity.SocialFb = model.SocialFb;
            entity.SocialInstagramm = model.SocialInstagramm;
            entity.SocialTwitter = model.SocialTwitter;
            entity.SocialVk = model.SocialVk;

            if (model.LastAuth > DateTime.MinValue)
                entity.LastAuth = model.LastAuth;

            entity.MinPictureId = model.MinPictureId;
            entity.OriginalPictureId = model.OriginalPictureId;

            DbContext.SaveChanges();

            return entity;
        }

        public IEnumerable<ProfileEntity> Get()
        {
            return Users.ToList();
        }

        public IEnumerable<ProfileEntity> Get(BaseModel model, Expression<Func<bool, ProfileEntity>> predicate)
        {
            throw new NotImplementedException();
        }

        public ProfileEntity Get(string ownerId)
        {
            ProfileEntity profile = null;

            if (!string.IsNullOrEmpty(ownerId))
            {
                profile = Users
                    .Include(x => x.Owner)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.OwnerId == ownerId);
            }

            return profile;
        }



        public IEnumerator<ProfileEntity> GetEnumerator()
        {
            return Users.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void UpdateOriginalPicture(int profileId, int pictureId)
        {
            if (profileId > 0 && pictureId > 0)
            {
                var user = Users.FirstOrDefault(x => x.Id == profileId);
                user.OriginalPictureId = pictureId;
                DbContext.SaveChanges();
            }
        }

        public void UpdateMiniaturePicture(int profileId, int pictureId)
        {
            if (profileId > 0 && pictureId > 0)
            {
                var user = Users.FirstOrDefault(x => x.Id == profileId);
                user.MinPictureId = pictureId;
                DbContext.SaveChanges();
            }
        }

        public void UpdateOwner(int id, string ownerId)
        {
            var profile = Users.FirstOrDefault(p => p.Id == id);
            profile.OwnerId = ownerId;
            DbContext.SaveChanges();
        }

        public void UpdateOwner(string oldOwnerId, string newOwnerId)
        {
            var profile = Users.FirstOrDefault(p => p.OwnerId == oldOwnerId);
            profile.OwnerId = newOwnerId;
            DbContext.SaveChanges();
        }

        public void UpdateLastAuth(string owner)
        {
            var profile = Users.FirstOrDefault(p => p.OwnerId == owner);
            profile.LastAuth = DateTime.Now;
            DbContext.SaveChanges();
        }

        private ProfileEntity Get(int id)
        {
            ProfileEntity profile = null;

            if (id > 0)
            {
                profile = Users
                    .Include(x => x.Owner)
                    .Include(x => x.ProfileSettings)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);

            }

            return profile;
        }

        public ProfileEntity Create(string ownerId, string email, string userName = null)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            ProfileEntity userProfile = new ProfileEntity();
            userProfile.OwnerId = ownerId;
            userProfile.Email = email;
            userProfile.GivenName = userName;
            userProfile.LastAuth = DateTime.Now;
            DbContext.UserProfile.Add(userProfile);
            DbContext.SaveChanges();
            return userProfile;
        }

        public ProfileEntity Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
