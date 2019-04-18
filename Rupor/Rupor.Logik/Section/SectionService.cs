using Rupor.Domain.Context;
using Rupor.Domain.Entities.Section;
using Rupor.Services.Core.Base.Models;
using Rupor.Services.Core.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
namespace Rupor.Logik.Section
{
    public class SectionService : ISectionService
    {
        public SectionEntity this[int id] => Get(id);

        public SectionEntity Edit(SectionEntity editedInstance)
        {
            SectionEntity entity = null;
            using (var context = new RuporDbContext())
            {
                if (editedInstance.Id > 0)
                {
                    entity = context.Sections.FirstOrDefault(x => x.Id == editedInstance.Id);
                }
                else
                {
                    entity = new SectionEntity();
                    entity = context.Sections.Add(entity);
                }
                entity.ImageId = editedInstance.ImageId;
                entity.IsActive = editedInstance.IsActive;
                entity.IsDelete = editedInstance.IsDelete;
                entity.Name = editedInstance.Name;
                entity.OnTop = editedInstance.OnTop;
                entity.Description = editedInstance.Description;
                context.SaveChanges();
            }

            return entity;
        }

        public IEnumerable<SectionEntity> Get(Func<SectionEntity, bool> expr)
        {
            IEnumerable<SectionEntity> sections = null;

            using (var context = new RuporDbContext())
            {
                if (expr != null)
                {
                    sections = context.Sections
                        .Where(s => !s.IsDefault)
                        .Where(expr)
                        .ToList();
                }
                sections = context.Sections
                    .Where(s => !s.IsDefault).ToList();
            }

            return sections;
        }

        public IEnumerable<SectionEntity> Get()
        {
            throw new NotImplementedException();
        }



        public IEnumerable<SectionEntity> Get(BaseModel filterModel, Func<SectionEntity, bool> expr = null)
        {
            IEnumerable<SectionEntity> sections = null;

            using (var context = new RuporDbContext())
            {
                var query = context.Sections
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
            }

            return sections;
        }

        public void Remove(SectionEntity entry)
        {
            throw new NotImplementedException();
        }

        private SectionEntity Get(int id)
        {
            SectionEntity entity = null;

            if (id > 0)
            {
                using (var context = new RuporDbContext())
                {
                    entity = context.Sections.FirstOrDefault(x => x.Id == id && !x.IsDefault);
                }
            }

            return entity;
        }
    }
}
