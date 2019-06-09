using Rupor.Domain.Entities.Article;
using System.Collections.Generic;

namespace Rupor.Services.Core.Article.Model
{
    public interface IArticleModel
    {
        int Id { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        ICollection<int> Sections { get; set; }
        ICollection<int> Tags { get; set; }     
        /// <summary>
        /// Статус из  Enum, ищет по ключу из ресурсов
        /// </summary>
        ArticleStatus ArticleStatus { get; set; }
        ArticleStatus LastStatus { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        bool IsDelete { get; set; }
        int TitleImageId { get; set; }
        int AuthorId { get; set; }
        int? EditorId { get; set; }    
    }

    public interface IArticleNewModel
    {
        int Id { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        ICollection<int> Sections { get; set; }
        int TitleImageId { get; set; }
        int AuthorId { get; set; }
        int? EditorId { get; set; }
    }
}
