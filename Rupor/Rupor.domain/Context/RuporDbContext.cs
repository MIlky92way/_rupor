using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.RssAgregate;
using Rupor.Domain.Entities.Section;
using Rupor.Domain.Entities.Tag;
using Rupor.Domain.Entities.User;
using System.Data.Entity;

namespace Rupor.Domain.Context
{
    public partial class RuporDbContext : IdentityDbContext<UserEntity>
    {
        private const string connectionName = "ruporcnt";
        public RuporDbContext() : base(connectionName)
        {
            Database
                .SetInitializer<RuporDbContext>(new MigrateDatabaseToLatestVersion<RuporDbContext, Migrations.Configuration>());
        }


        //public DbSet<ReferenceSectionEntity> ReferenceSection { get; set; }
        //public DbSet<ReferenceEntity> Reference { get; set; }

        public DbSet<ArticleEntity> Article { get; set; }
        public DbSet<TagEntity> Tag { get; set; }
        public DbSet<FeedChannelEntity> FeedChannel { get; set; }
        public DbSet<FeedEntity> RssFeed { get; set; }
        public DbSet<ProfileEntity> UserProfile { get; set; }
        public DbSet<SectionSettingsEntity> SectionSettings { get; set; }
        public DbSet<SectionEntity> Sections { get; set; }
        public DbSet<FileEntity> Files { get; set; }
    }
}
