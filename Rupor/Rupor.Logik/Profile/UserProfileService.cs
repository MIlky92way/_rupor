using Rupor.Domain.Context;
using Rupor.Domain.Entities.User;
using Rupor.Logik.Base.Model;
using Rupor.Logik.Profile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Rupor.Logik.Profile
{
    public class UserProfileService : IUserProfileService<ProfileEntity>
    {
        private RuporDbContext dbContext;

        public ProfileEntity this[int id] => Get(id);

        public ProfileEntity this[string email]
        {
            get
            {
                ProfileEntity entity = null;

                using (dbContext = new RuporDbContext())
                {
                    entity = dbContext
                        .UserProfile
                        .FirstOrDefault(x => x.Email == email);
                }

                return entity;
            }
        }


        public ProfileEntity Edit(ProfileEntity model)
        {
            ProfileEntity entity = null;

            using (dbContext = new RuporDbContext())
            {
                if (model.Id > 0)
                {
                    entity = dbContext
                        .UserProfile
                        .Include(x => x.Owner)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == model.Id);
                }
                else
                {
                    entity = new ProfileEntity();
                    dbContext.UserProfile.Add(entity);
                }

                entity.OwnerId = model.OwnerId;
                entity.GivenName = model.GivenName;
                entity.FamilyName = model.FamilyName;
                entity.Patronymic = model.Patronymic;
                entity.LastAuth = model.LastAuth;
                entity.BirthDate = model.BirthDate;
                entity.FileNameOriginal = model.FileNameOriginal;
                entity.FileNameMin = model.FileNameMin;
                entity.Email = model.Email;

                entity.MinPictureId = model.MinPictureId;
                entity.OriginalPictureId = model.OriginalPictureId;

                dbContext.SaveChanges();
            }

            return entity;
        }

        public IEnumerable<ProfileEntity> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProfileEntity> Get(BaseModel model, Expression<Func<bool, ProfileEntity>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProfileEntity entry)
        {
            if (entry == null)
            {
                return;
            }

            using (dbContext = new RuporDbContext())
            {
                try
                {
                    dbContext.Set<ProfileEntity>().Attach(entry);
                    dbContext.UserProfile.Remove(entry);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //TODO - изменить обновление фото профиля!
        public void UpdatePictue(PictureModel model)
        {
            ProfileEntity entry = null;

            if (model.ProfileId == 0)
            {
                return;
            }

            using (dbContext = new RuporDbContext())
            {
                entry = this[model.ProfileId];

                entry.FileNameOriginal = model.UrlToOriginalImage;
                entry.FileNameMin = model.UrlToMinImage;

                dbContext.SaveChanges();
            }
        }

        private ProfileEntity Get(int id)
        {
            ProfileEntity profile = null;

            if (id > 0)
            {
                using (dbContext = new RuporDbContext())
                {
                    profile = dbContext.UserProfile
                        .Include(x => x.OriginalPicture)
                        .Include(x => x.MinPicture)
                        .Include(x => x.Owner)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Id == id);
                }
            }

            return profile;
        }

    }
}
