using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Ioc.Container
{
    public class Container
    {
        public static void RegisterIoc(Assembly assembly)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(assembly);

            var init = new MvcRegisterTypes(builder);

        }
    }
}
