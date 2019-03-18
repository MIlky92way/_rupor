using Autofac;
using Rupor.Domain.Entities.User;
using Rupor.Logik.File;
using Rupor.Logik.Profile;
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
        }
    }
}
