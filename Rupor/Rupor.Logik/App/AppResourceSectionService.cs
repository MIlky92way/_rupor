using Rupor.Domain.Entities.Resources;
using Rupor.Logik.Base;
using Rupor.Services.Core.App;
using Rupor.Services.Core.App.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Rupor.Logik.App
{
    public class AppResourceSectionService : EFBaseService, IAppResourceSectionService
    {
        private DbSet<AppResourceSectionEntity> Sections { get; set; }

        private IQueryable<AppResourceSectionEntity> Quryable => Sections;

        public Expression Expression => Quryable.Expression;

        public Type ElementType => Quryable.ElementType;

        public IQueryProvider Provider => Quryable.Provider;

        public AppResourceSectionEntity this[int id] => Sections.Find(id);

        public AppResourceSectionService()
        {
            Sections = DbContext.Set<AppResourceSectionEntity>();
        }

        /// <summary>
        /// Редактирует раздел ресурсов приложения.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AppResourceSectionEntity Edit(IAppResourceSectionModel model)
        {
            AppResourceSectionEntity entity = null;
            if (model.Id > 0)
            {
                entity = Sections.Find(model.Id);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entity = new AppResourceSectionEntity();
                Sections.Add(entity);
            }

            entity.Name = model.Name;
            entity.Description = model.Description;
            DbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var section = Sections.Find(id);

            if (section != null)
            {
                Sections.Remove(section);
                DbContext.SaveChanges();
            }
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public IEnumerator<AppResourceSectionEntity> GetEnumerator()
        {
            return Quryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Quryable.GetEnumerator();
        }
       
        public AppResourceSectionEntity Find(int id)
        {
            return Sections.Find(id);
        }


        /// <summary>
        /// DO NOT USE!!!
        /// </summary>
        /// <param name="editedInstance"></param>
        /// <returns></returns>
        public AppResourceSectionEntity Edit(AppResourceSectionEntity editedInstance)
        {
            throw new NotImplementedException();
        }
    }
}
