using System;
using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.Tag;
using System.Data.Entity;
using Rupor.Domain.Entities.RssAgregate;

namespace Rupor.Domain.Context
{
    public partial class RuporDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigArticleTagEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigArticleTagEntity(DbModelBuilder builder)
        {
            builder.Entity<ArticleEntity>()
                .HasMany<TagEntity>(x => x.Tags)
                .WithMany(x => x.Articles)
                .Map(e =>
                {
                    e.MapLeftKey("ArticleId");
                    e.MapRightKey("TagId");
                    e.ToTable("ArticleTag");
                });

            builder.Entity<FeedEntity>()
                .HasMany<TagEntity>(x => x.Tags)
                .WithMany(x => x.RssFeeds)
                .Map(e =>
                {
                    e.MapLeftKey("RssFeedId");
                    e.MapRightKey("TagId");
                    e.ToTable("RssFeedTag");
                });
        }

        public static  RuporDbContext GetInstance()
        {
            return new RuporDbContext();
        }
    }
}
