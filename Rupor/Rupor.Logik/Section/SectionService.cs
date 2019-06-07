using Rupor.Domain.Entities.Section;
using Rupor.Logik.Base;
using Rupor.Services.Core.Base.Models;
using Rupor.Services.Core.Section;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
namespace Rupor.Logik.Section
{
    public class SectionService : EFBaseService, ISectionService
    {
        public SectionEntity this[int id] => Get(id);

        private DbSet<SectionEntity> Sections { get; set; }
        public SectionService()
        {
            Sections = DbContext.Set<SectionEntity>();
        }
        public SectionEntity Edit(SectionEntity editedInstance)
        {
            SectionEntity entity = null;

            if (editedInstance.Id > 0)
            {
                entity = Sections.FirstOrDefault(x => x.Id == editedInstance.Id);
            }
            else
            {
                entity = new SectionEntity();
                entity = Sections.Add(entity);
            }
            entity.ImageId = editedInstance.ImageId;
            entity.IsActive = editedInstance.IsActive;
            entity.IsDelete = editedInstance.IsDelete;
            entity.Name = editedInstance.Name;
            entity.OnAside = editedInstance.OnAside;
            entity.Description = editedInstance.Description;
            DbContext.SaveChanges();


            return entity;
        }

        public IEnumerable<SectionEntity> Get(Func<SectionEntity, bool> expr)
        {
            IEnumerable<SectionEntity> sections = null;
            {
                if (expr != null)
                {
                    sections = Sections
                        .Where(s => !s.IsDefault)
                        .Where(expr)
                        .ToList();
                }
                sections = Sections;
            }

            return sections;
        }

        public IEnumerable<SectionEntity> Get(BaseModel filterModel, Func<SectionEntity, bool> expr = null)
        {
            IEnumerable<SectionEntity> sections = null;

            var query = Sections
                .AsNoTracking()
                .AsQueryable();

            filterModel.Total = query.Count();

            if (expr != null)
            {
                query = query
                    .Where(expr)
                    .AsQueryable();
            }
            query = query
                  .Where(s => !s.IsDefault);

            if (!filterModel.IsAscending)
                query = query.OrderBy(filterModel.FieldOrderBy + " DESC ");
            else
                query = query.OrderBy(filterModel.FieldOrderBy + " ASC ");

            sections = query
                .Skip((filterModel.Page - 1) * filterModel.CountOnPage)
                .Take(filterModel.CountOnPage)
                .ToList();

            return sections;
        }

        private SectionEntity Get(int id)
        {
            SectionEntity entity = null;

            if (id > 0)
            {
                entity = Sections.FirstOrDefault(x => x.Id == id && !x.IsDefault);
            }

            return entity;
        }

        public IEnumerable<SectionEntity> GetDefaults() => Sections.Where(c => c.IsDefault);



        public void Remove(int id)
        {
            if (id > 0)
            {
                var section = Sections.FirstOrDefault(s => s.Id == id);
                Sections.Remove(section);
                DbContext.SaveChanges();
            }

        }

        public IEnumerator<SectionEntity> GetEnumerator()
        {
            return Sections.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void ChangeActiveState(int id, bool? isActive)
        {
            if (id > 0)
            {
                var section = Sections.FirstOrDefault(s => s.Id == id);
                if (section != null)
                {
                    section.IsActive = isActive.Value;
                    DbContext.SaveChanges();
                }
            }
        }

        public void ChangeDeleteState(int id, bool? IsDelete)
        {
            if (id > 0)
            {
                var section = Sections.FirstOrDefault(s => s.Id == id);
                if (section != null)
                {
                    section.IsDelete = IsDelete.Value;
                    DbContext.SaveChanges();
                }
            }
        }

        public SectionEntity Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
