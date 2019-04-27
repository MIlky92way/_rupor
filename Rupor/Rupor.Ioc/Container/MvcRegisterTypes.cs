using Autofac;
using Rupor.Domain.Entities.User;
using Rupor.Logik.Common;
using Rupor.Logik.Feed;
using Rupor.Logik.File;
using Rupor.Logik.Profile;
using Rupor.Logik.Section;
using Rupor.Services.Core.Common;
using Rupor.Services.Core.Feed;
using Rupor.Services.Core.File;
using Rupor.Services.Core.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Ioc.Container
{
    public class MvcRegisterTypes
    {
        private ContainerBuilder builder;
        public MvcRegisterTypes(ContainerBuilder builder)
        {
            this.builder = builder;
        }
        public void InitDependencies()
        {
            //builder.RegisterAssemblyTypes(typeof(ProblemType).Assembly)
            //    .AsImplementedInterfaces();

            builder.RegisterType<FileService>().As<IFileService>();            

            builder.RegisterType<UserProfileService>().PropertiesAutowired(PropertyWiringOptions.None);

            builder.RegisterType<SectionSettingsService>().As<ISectionSettingsService > ()
                .InstancePerRequest();

            builder.RegisterType<SectionService>().As<ISectionService>()
                .InstancePerRequest();
            builder.RegisterType<FeedChannelService>().As<IFeedChannelService>()
                .InstancePerRequest();

            builder.RegisterType<ServiceCore>().As<IServiceCore>()
                .InstancePerRequest();
        }
    }
}
