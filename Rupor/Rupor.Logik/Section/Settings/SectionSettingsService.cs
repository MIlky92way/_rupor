using Rupor.Domain.Context;
using Rupor.Domain.Entities.Section;
using Rupor.Services.Core.Section;
using Rupor.Services.Core.Section.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Rupor.Logik.Section
{
    public class SectionSettingsService : ISectionSettingsService
    {
        public SectionSettingsModel Settings => Get();

        public IList<SectionEntity> DefaultSections
        {
            get => new RuporDbContext().Sections.Where(s => s.IsDefault).ToList();
        }

        public void Edit(SectionSettingsModel model)
        {

            using (var ctx = new RuporDbContext())
            {
                try
                {
                    if (model.SettingsArea == SectionSettingArea.Settings)
                    {
                        EditSectionSettings(ctx, model);
                    }

                    if (model.SettingsArea == SectionSettingArea.DefaultSections)
                    {
                        EditDefaultSection(ctx, model);
                    }

                    ctx.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    //TODO log
                    throw ex;
                }
                catch (Exception ex)
                {
                    //TODO log
                    throw ex;
                }
            }
        }

        public SectionEntity GetDefaultSection(int id)
        {
            SectionEntity section = null;

            if (id > 0)
            {
                using (var ctx = new RuporDbContext())
                {
                    section = ctx.Sections.FirstOrDefault(s => s.Id == id && s.IsDefault);
                }
            }

            return section;
        }

        public void RemoveDefaultSection(int id)
        {            
            if (id > 0)
            {
                using (var ctx = new RuporDbContext())
                {
                   var section = ctx.Sections.FirstOrDefault(s => s.Id == id && s.IsDefault);
                   ctx.Sections.Remove(section);
                   ctx.SaveChanges();
                }
            }
        }

        #region edit tool

        private void EditDefaultSection(RuporDbContext context, SectionSettingsModel model)
        {
            SectionEntity section = null;
            if (model.SectionId > 0)
            {
                section = context.Sections.FirstOrDefault(s => s.Id == model.SectionId);
                if (section == null)
                {
                    AttachSection(context, section);
                }
            }
            else
            {
                if (!CheckCount(model.MaxAllowedSections))
                {
                    throw new InvalidOperationException("Cannot create than maximum allowed!");
                }

                section = AttachSection(context, section);
            }

            if (!ExistSettings())
            {
                throw new InvalidOperationException("Cannot create default section, because not exists default setting for sections!");
            }


            section.IsActive = model.IsActive;
            section.IsDefault = true;
            section.Name = model.Name;
            section.Description = model.Description;
            section.OnTop = model.OnTop;

            bool CheckCount(int max)
            {
                var count = context.Sections
                    .Where(s => s.IsDefault).Count();

                return count < max;
            }

            bool ExistSettings() => context.SectionSettings
                                        .OrderByDescending(s => s.DateCreate)
                                        .FirstOrDefault() != null;
        }

        private void EditSectionSettings(RuporDbContext context, SectionSettingsModel model)
        {
            SectionSettingsEntity settingsEntity = null;

            if (model.Id > 0)
            {
                settingsEntity = context.SectionSettings
                    .FirstOrDefault(s => s.Id == model.Id);

                if (settingsEntity == null)
                {
                    settingsEntity = AttachSectionSettings(context, settingsEntity);
                }
            }
            else
            {
                settingsEntity = AttachSectionSettings(context, settingsEntity);
            }

            settingsEntity.DefaultPictureId = model.DefaultPictureId;
            settingsEntity.MaxAllowedSections = model.MaxAllowedSections;
        }



        private SectionSettingsEntity AttachSectionSettings(RuporDbContext context, SectionSettingsEntity entity)
        {
            entity = entity ?? new SectionSettingsEntity();
            context.SectionSettings.Add(entity);
            return entity;
        }

        private SectionEntity AttachSection(RuporDbContext context, SectionEntity entity)
        {
            entity = entity ?? new SectionEntity();
            context.Sections.Add(entity);
            return entity;
        }


        #endregion

        #region getters

        private SectionSettingsModel Get()
        {
            var model = new SectionSettingsModel();

            using (var ctx = new RuporDbContext())
            {
                var settings = ctx.SectionSettings
                    .OrderByDescending(s => s.DateCreate)
                    .FirstOrDefault();

                if (settings == null)
                {
                    return null;
                }
                model.DefaultSections = ctx.Sections
                    .Where(s => s.IsDefault)
                    .OrderByDescending(s => s.DateCreate).ToList();

                model.DefaultPictureId = settings.DefaultPictureId;
                model.MaxAllowedSections = settings.MaxAllowedSections;
            }

            return model;
        }

        public void RemoveDefaultSettings()
        {
            using (var ctx = new RuporDbContext())
            {
                try
                {
                    var sections = ctx.SectionSettings;
                    ctx.SectionSettings.RemoveRange(sections);
                    ctx.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw ex;
                    //TODO LOG
                }
                catch (Exception ex)
                {
                    throw ex;
                    //TODO LOG
                }
            }
        }

        #endregion

        #region tools



        #endregion
    }
}
