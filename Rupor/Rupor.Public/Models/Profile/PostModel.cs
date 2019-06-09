using Rupor.Domain.Entities.Article;
using Rupor.Services.Core.Article;
using Rupor.Services.Core.Article.Model;
using System;
using System.Collections.Generic;

namespace Rupor.Public.Models.Profile
{
    public class PostModel: IArticleNewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public int AuthorId { get; set; }
        public int TitleImageId { get; set; }
        public bool IsDraft { get; set; }
        public ICollection<int> Sections { get; set; }
        public int? EditorId { get; set; }

        public PostModel()
        {

        }

        public PostModel(ArticleEntity article,IArticleService service)
        {
            Id = article.Id;
            Title = article.Title;
            AuthorId = article.AuthorId;
            TitleImageId = article.TitleImageId ?? 0;
            Content = article.Content;
            Status = service.GetDisplayStatus(article.CurrentModerationStatus);            
        }
    }
}