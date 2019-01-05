using Microsoft.AspNet.Identity.EntityFramework;
using Rupor.Domain.Entities.Article;
using Rupor.Domain.Entities.Reference;
using Rupor.Domain.Entities.Tag;
using Rupor.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.domain.Context
{
    public partial class RuporDbContext:IdentityDbContext<UserEntity>
    {
        public RuporDbContext()
        {
            Database
                .SetInitializer<RuporDbContext>(new MigrateDatabaseToLatestVersion<RuporDbContext, Migrations.Configuration>());
        }
        public DbSet<ReferenceSectionEntity> ReferenceSection { get; set; }
        public DbSet<ReferenceEntity> Reference { get; set; }
        public DbSet<ArticleEntity> Article { get; set; }
        public DbSet<TagEntity> Tag { get; set; }

    }
}
