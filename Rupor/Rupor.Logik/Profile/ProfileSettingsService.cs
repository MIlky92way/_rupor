using Rupor.Domain.Entities.User;
using Rupor.Logik.Base;
using Rupor.Services.Core.Profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Rupor.Logik.Profile
{
    public class ProfileSettingsService : EFBaseService, IProfileSettngs
    {
        protected virtual DbSet<ProfileSettingsEntity> DbSettings { get; set; }

        public ProfileSettingsEntity this[int id, int profileId] =>
            DbContext
            .ProfileSettings
            .FirstOrDefault(s => s.Id == id && s.ProfileId == profileId);

        public IEnumerable<ProfileSettingsEntity> Settings { get; }

        public ProfileSettingsService()
        {
            DbSettings = DbContext.Set<ProfileSettingsEntity>();

            Settings = DbSettings.ToList();
        }

        public ProfileSettingsEntity Create(int profileId)
        {
            ProfileSettingsEntity settings = new ProfileSettingsEntity();

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    settings.ProfileId = profileId;
                    settings = DbContext.ProfileSettings.Add(settings);
                    var profile = DbContext.UserProfile.FirstOrDefault(u => u.Id == profileId);
                    profile.ProfileSettingsId = settings.Id;
                    DbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }

            }

            return settings;
        }

        public void SubscribeOnAuthors(int profileId, IList<int> authorIds, int id = 0)
        {
            if (profileId == 0)
                throw new ArgumentException(nameof(profileId));

            ProfileSettingsEntity settings = null;

            if (id > 0)
                settings = DbSettings.Find(id);
            else
            {
                settings = new ProfileSettingsEntity();
                settings.ProfileId = profileId;
            }

            foreach (var item in authorIds ?? new List<int>())
            {
                var profile = DbContext.UserProfile.Find(item);

                if (profile != null)
                {
                    settings.SubscribeOnAuthors?.Add(profile);
                }
            }

            DbContext.SaveChanges();
        }

        public void SubscribeOnSections(int profileId, IList<int> sectionIds, int id = 0)
        {
            if (profileId == 0)
                throw new ArgumentException(nameof(profileId));

            ProfileSettingsEntity settings = null;

            if (id > 0)
                settings = DbSettings.Find(id);
            else
            {
                settings = new ProfileSettingsEntity();
                settings.ProfileId = profileId;
            }

            foreach (var item in sectionIds ?? new List<int>())
            {
                var section = DbContext.Sections.Find(item);

                if (section != null)
                {
                    settings.SubscribeOnSections?.Add(section);
                }
            }

            DbContext.SaveChanges();
        }

        public void UnSubscribeOnAuthors(int id, int profileId, IList<int> authorIds)
        {
            if (id == 0)
                throw new ArgumentException(nameof(id));
            if (profileId == 0)
                throw new ArgumentException(nameof(profileId));

            ProfileSettingsEntity settings = null;

            settings = DbSettings.Find(id);

            foreach (var item in authorIds ?? new List<int>())
            {
                var userPofile = DbContext.UserProfile.Find(item);

                if (userPofile != null)
                {
                    settings.SubscribeOnAuthors?.Remove(userPofile);
                }
            }

            DbContext.SaveChanges();
        }

        public void UnSubscribeOnSections(int id, int profileId, IList<int> sectionIds)
        {
            if (id == 0)
                throw new ArgumentException(nameof(id));
            if (profileId == 0)
                throw new ArgumentException(nameof(profileId));

            ProfileSettingsEntity settings = null;

            settings = DbSettings.Find(id);

            foreach (var item in sectionIds ?? new List<int>())
            {
                var section = DbContext.Sections.Find(item);

                if (section != null)
                {
                    settings.SubscribeOnSections?.Remove(section);
                }
            }

            DbContext.SaveChanges();
        }
    }
}
