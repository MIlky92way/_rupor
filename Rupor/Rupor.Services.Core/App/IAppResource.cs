using Rupor.Domain.Entities.Resources;
using Rupor.Services.Core.App.Model;
using Rupor.Services.Core.Base;
using System;
using System.Linq;

namespace Rupor.Services.Core.App
{
    public interface IAppResourceService : IQueryable<AppResourceEntity>, IRuporService<AppResourceEntity>
    {
        /// <summary>
        /// Редактирует ресурс приложения
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns></returns>        
        AppResourceEntity Edit(IAppResourceModel model);

        /// <summary>
        /// Сохраняет измененный объект с последующим возвратом кол-ва обработанных строк
        /// </summary>
        /// <returns></returns>
        int Save();
    }

    public interface IAppResourceSectionService : IQueryable<AppResourceSectionEntity>, IRuporService<AppResourceSectionEntity>
    {
        /// <summary>
        /// Редактирует раздел ресурса приложения
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns></returns>
        AppResourceSectionEntity Edit(IAppResourceSectionModel model);
        void Delete(int id);
        /// <summary>
        /// Сохраняет измененный объект с последующим возвратом кол-ва обработанных строк
        /// </summary>
        /// <returns></returns>
        int Save();
    }
}
