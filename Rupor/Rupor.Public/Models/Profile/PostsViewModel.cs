using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.User;
using Rupor.Public.Infrastructure.ProfileTools;
using Rupor.Services.Core.Article;
using Rupor.Tools.Consts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rupor.Public.Models.Profile
{
    public class PostsViewModel
    {
        public IEnumerable<ArticleModel> Articles { get; set; }
        public bool IsCurrent { get; set; }

        public PostsViewModel(IArticleService service, ProfileWeb current, ProfileEntity viewProfile = null, ArticleStatus status = ArticleStatus.Approved)
        {
            var query = service
                .Where(x => x.IsActive && !x.IsDelete);

            if (!current.IsGuest && current.Roles.Any(r => r.Name == Role._USER) && viewProfile != null)
            {
                query = query.Where(x => x.AuthorId == viewProfile.Id);
            }
            else if (current.Roles.Any(r => r.Name == Role._RUBRICATOR || r.Name == Role._ADMINISTRATOR || r.Name == Role._ROOT))
            {
                query = query.Where(x => x.AuthorId == current.Id);
                IsCurrent = true;
            }
            else
            {
                throw new ArgumentNullException(nameof(viewProfile));
            }

            Articles = query
                .Select(x => new ArticleModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    AuthorId = x.Author.Id,
                    AuthorImageId = x.Author.OriginalPictureId ?? 0,
                    AuthorName = x.Author.GivenName ?? x.Author.Email ?? "n/a",
                    DateCreate = x.DateCreate,
                    TitleImageId = x.TitleImageId ?? 0,
                    Sections = x.Sections
                    .Select(s => new ArticleModel.SectionDto { Id = s.Id, Name = s.Name }).ToList()
                });
        }
    }

    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public int TitleImageId { get; set; }
        public int AuthorImageId { get; set; }
        public DateTime DateCreate { get; set; }
        public string Status { get; set; }

        public IList<SectionDto> Sections { get; set; }

        public class SectionDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}