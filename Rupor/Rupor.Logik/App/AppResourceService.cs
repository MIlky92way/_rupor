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
    public class AppResourceService : EFBaseService, IAppResourceService
    {

        private DbSet<AppResourceEntity> Resources { get; set; }
        private IQueryable<AppResourceEntity> Quryable => Resources;

        public AppResourceService()
        {
            Resources = DbContext.Set<AppResourceEntity>();
        }

        public AppResourceEntity this[int id] => Resources.Find(id);

        public Expression Expression => Quryable.Expression;

        public Type ElementType => Quryable.ElementType;

        public IQueryProvider Provider => Quryable.Provider;

        public AppResourceEntity Edit(IAppResourceModel model)
        {
            AppResourceEntity entity = null;
            if (model.Id > 0)
            {
                entity = Resources.Find(model.Id);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entity = new AppResourceEntity();
                Resources.Add(entity);
            }

            //entity.Key = model.Key;
            entity.Value = model.Value;
            entity.SectionId = model.SectionId;

            Save();

            return entity;
        }

        /// <summary>
        /// DO NOT USE
        /// </summary>
        /// <param name="editedInstance"></param>
        /// <returns></returns>
        public AppResourceEntity Edit(AppResourceEntity editedInstance)
        {
            throw new NotImplementedException();
        }

        public AppResourceEntity Find(int id)
        {
            return Resources.Find(id);
        }

        public IEnumerator<AppResourceEntity> GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Save()
        {
           return DbContext.SaveChanges();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Quryable.GetEnumerator();
        }
    }
}
