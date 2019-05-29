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
        ICollection<int> Images { get; set; }
        /// <summary>
        /// Статус из  Enum, ищет по ключу из ресурсов
        /// </summary>
        int ArticleStatus { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        bool IsDelete { get; set; }
        int TitleImageId { get; set; }
        int AuthorId { get; set; }
        int? EditorId { get; set; }
        int StatusId { get; set; }
    }
}
