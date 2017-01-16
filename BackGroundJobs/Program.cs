using BackGroundJobs.Services;
using Topshelf;
using Autofac;
using BackGroundJobs.Config;
using Topshelf.Autofac;
using Topshelf.Common.Logging;

namespace BackGroundJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.ConfigureContainer();

            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.UseCommonLogging();
                hostConfigurator.UseAutofacContainer(container);
                hostConfigurator.Service(container.Resolve<MainService>);

                hostConfigurator.SetDisplayName("BackGroundJobsProcessor");
                hostConfigurator.SetDescription("BackGroundJobsProcessor");
                hostConfigurator.SetServiceName("BackGroundJobsProcessor");
            });
        }
    }
}
