using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.RssAgregate;
using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.Tag;
using Rupor.Domain.Entities.User;
using System.Data.Entity;

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

            builder.Entity<ProfileSettingsEntity>()
                .HasMany<SectionEntity>(x => x.SubscribeOnSections)
                .WithMany(x => x.ProfileSettings)
                .Map(e =>
                {
                    e.MapLeftKey("SettingsId");
                    e.MapRightKey("SectionId");
                    e.ToTable("ProfileSettingsSection");
                });
        }

        public static RuporDbContext GetInstance()
        {
            return new RuporDbContext();
        }
    }
}
