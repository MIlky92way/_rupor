using Rupor.Domain.Entities.Article;
using Rupor.Services.Core.Article.Model;
using System.Linq;

namespace Rupor.Services.Core.Article
{
    /// <summary>
    /// Сервис по работе со статьями
    /// </summary>
    public interface IArticleService : IQueryable<ArticleEntity>
    {
        /// <summary>
        /// Получает статью по её идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleEntity this[int id] { get; }       
        ArticleEntity Edit(IArticleModel model);
        ArticleEntity Create(int authorId);
        ArticleEntity CreateNew(IArticleNewModel model);
        int Save();
        void Remove(int id);

        string GetDisplayStatus(ArticleStatus status);
    }
}
