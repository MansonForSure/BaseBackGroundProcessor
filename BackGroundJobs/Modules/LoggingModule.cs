using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Common.Logging;
using Module = Autofac.Module;

namespace BackGroundJobs.Modules
{
    public class LoggingModule : Module
    {
        protected override void AttachToComponentRegistration(Autofac.Core.IComponentRegistry componentRegistry, Autofac.Core.IComponentRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }
            registration.Preparing += RegistrationOnPreparing;
        }

        private static void RegistrationOnPreparing(object sender, PreparingEventArgs preparingEventArgs)
        {
            var t = preparingEventArgs.Component.Activator.LimitType;

            preparingEventArgs.Parameters = preparingEventArgs.Parameters.Union(new[]
                {
                    new ResolvedParameter((p, i) => p.ParameterType == typeof(ILog), (p, i) =>
                    {
                        var logger = CreateLogger(t);
                        return logger;
                    })
                });
        }

        public static ILog CreateLogger(Type type)
        {
            var logger = LogManager.GetLogger(type);
            return logger;
        }
    }
}
