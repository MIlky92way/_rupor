using Rupor.Domain.Entities.Article;
using Rupor.Services.Core.Article.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Article
{
    public interface IArticleService:IQueryable<ArticleEntity>
    {
        ArticleEntity Edit(IArticleModel model);
    }
}
