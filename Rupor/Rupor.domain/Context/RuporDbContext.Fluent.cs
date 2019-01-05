using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.Tag;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.domain.Context
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
        }
    }
}
