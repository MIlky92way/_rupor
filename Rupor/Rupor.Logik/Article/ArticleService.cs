using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.Resources;
using Rupor.Logik.Base;
using Rupor.Services.Core.Article;
using Rupor.Services.Core.Article.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Rupor.Logik.Article
{
    public class ArticleService : EFBaseService, IArticleService
    {
        private DbSet<ArticleEntity> Articles { get; set; }
        private IQueryable<AppResourceEntity> ArticlesStatuse { get; set; }

        private int StatusDraft => ArticlesStatuse
            .FirstOrDefault(r => r.Key == (int)ArticleStatus.Draft)?.Id ?? throw new NullReferenceException("status article not found!");

        private int StatusNew => ArticlesStatuse
          .FirstOrDefault(r => r.Key == (int)ArticleStatus.New)?.Id ?? throw new NullReferenceException("status article not found!");

        public ArticleService()
        {
            Articles = DbContext.Article;

            var sectionResource = DbContext
                .AppResousrceSection
                .FirstOrDefault(x => x.Key == SectionResource.ArticleStatus) ??
                throw new NullReferenceException("application resource for article stastus not found!");

            ArticlesStatuse =
                DbContext.
                AppResource.
                Where(r => r.SectionId == sectionResource.Id);
        }

        public ArticleEntity this[int id]
        {
            get
            {
                var art = Articles.Find(id);
                var currentStatus = ArticlesStatuse.FirstOrDefault(s => s.Id == art.StatusId);

                if (currentStatus != null)
                {
                    art.CurrentModerationStatus = (ArticleStatus)Enum
                        .Parse(typeof(ArticleStatus), currentStatus.Key.ToString());
                }
                return art;
            }
        }

        public Expression Expression => Articles.AsQueryable().Expression;

        public Type ElementType => Articles.AsQueryable().ElementType;

        public IQueryProvider Provider => Articles.AsQueryable().Provider;


        public ArticleEntity Edit(IArticleModel model)
        {
            var article = DbContext.Article.Find(model.Id);

            if (article == null)
            {
                article = new ArticleEntity();
            }

            article.AuthorId = model.AuthorId;
            article.StatusId = GetStatus(model.ArticleStatus);            
            article.Title = model.Title;
            article.Content = model.Content;
            article.TitleImageId = model.TitleImageId;

            if (!string.IsNullOrEmpty(model.Description))
                article.Description = model.Description;

            if (model.EditorId > 0)
                article.EditorId = model.EditorId;

            var sections = article.Sections;

            HandleSections();

            void HandleSections()
            {
                foreach (var section in sections)
                {
                    sections.Remove(section);
                }

                foreach (var sectionId in model.Sections ?? new List<int>())
                {
                    var section = DbContext.Sections.Find(sectionId);
                    article.Sections.Add(section);
                }
            }

            DbContext.SaveChanges();

            return article;
        }

        public IEnumerator<ArticleEntity> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Articles.AsQueryable().GetEnumerator();
        }

        public ArticleEntity Create(int authorId)
        {
            var article = new ArticleEntity();
            article.AuthorId = authorId;
            article.IsActive = true;
            article.StatusId = StatusDraft;
            article.EditorId = authorId;
            article.LastModify = DateTime.Now;

            article = DbContext.Article.Add(article);
            DbContext.SaveChanges();

            return article;
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayStatus(ArticleStatus status)
        {
            string displayStatus = "";
            switch (status)
            {
                case ArticleStatus.New:
                    displayStatus = "Новая";
                    break;
                case ArticleStatus.Moderation:
                    displayStatus = "На модерации";
                    break;
                case ArticleStatus.Approved:
                    displayStatus = "Подтверждена";
                    break;
                case ArticleStatus.Canceled:
                    displayStatus = "Отменена";
                    break;
                case ArticleStatus.Publicated:
                    displayStatus = "Опубликована";
                    break;
                case ArticleStatus.Archive:
                    displayStatus = "В архиве";
                    break;
                case ArticleStatus.Draft:
                default:
                    displayStatus = "Черновик";
                    break;
            }

            return displayStatus;
        }

        public ArticleEntity CreateNew(IArticleNewModel model)
        {
            throw new NotImplementedException();
        }

        private int GetStatus(ArticleStatus status)
        {
            return ArticlesStatuse
             .FirstOrDefault(r => r.Key == (int)status)?.Id ?? throw new NullReferenceException("application resource for article stastus not found!");
        }
    }
}
