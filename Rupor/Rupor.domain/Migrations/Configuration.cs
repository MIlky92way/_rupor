using Rupor.domain.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.domain.Migrations
{
    public class Configuration: DbMigrationsConfiguration<RuporDbContext>
    {
        public Configuration()
        {
            Database
                .SetInitializer<RuporDbContext>(new MigrateDatabaseToLatestVersion<RuporDbContext, Migrations.Configuration>());
        }

        protected override void Seed(RuporDbContext context)
        {
            base.Seed(context);
        }

    }
}
