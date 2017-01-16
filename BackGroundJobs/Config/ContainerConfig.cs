using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BackGroundJobs.Modules;
using BackGroundJobs.Processors.Interfaces;
using BackGroundJobs.Services;
using NLog.Internal;

namespace BackGroundJobs.Config
{
    public static class ContainerConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var callingAssembly = System.AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterType<MainService>().InstancePerDependency();
            builder.RegisterModule<LoggingModule>();
            builder.RegisterAssemblyTypes(typeof(IBaseProcessor).Assembly);

            //--------------Build----------------------------//
            var container = builder.Build();
            return container;
        }
    }
}
