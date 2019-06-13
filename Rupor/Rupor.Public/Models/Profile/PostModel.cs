using Rupor.Domain.Entities.Article;
using Rupor.Services.Core.Article.Model;
using Rupor.Services.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Rupor.Public.Models.Profile
{
    public class PostModel : IArticleModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// Строковое представление статуса
        /// </summary>
        public string Status { get; set; }
        [Required]
        public int AuthorId { get; set; }

        public int TitleImageId { get; set; }
        public bool IsDraft { get; set; }
        public ICollection<int> Sections { get; set; }
        public int? EditorId { get; set; }
        public ICollection<int> Tags { get; set; }

        public ArticleStatus ArticleStatus { get; set; }
        public ArticleStatus LastStatus { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public SelectList SelectSections { get; set; }
        public PostModel()
        {

        }

        public PostModel(ArticleEntity article, IServiceCore service)
        {
            var articleSrv = service.ArticleService;

            Id = article.Id;
            Title = article.Title;
            AuthorId = article.AuthorId;
            TitleImageId = article.TitleImageId ?? 0;
            Content = article.Content;
            Status = articleSrv.GetDisplayStatus(article.CurrentModerationStatus);

            SelectSections = new SelectList(service.SectionService.Where(x => x.IsActive && !x.IsDefault), "Id", "Name");
        }
    }
}