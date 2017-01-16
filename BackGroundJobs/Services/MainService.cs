using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using BackGroundJobs.Processors.Interfaces;

using Common.Logging;
using Topshelf;

namespace BackGroundJobs.Services
{
    class MainService : ServiceControl
    {
        private readonly ILog _log;
        protected ILifetimeScope _lifetimeScope;
        private int _interval;
        protected List<Task> _workerTasks;
        protected List<IBaseProcessor> _Processors;

        private readonly Task _task;
        private bool _docancel = false;
        private void BuildWorkerTasks()
        {

            _Processors = new List<IBaseProcessor>();

            NameValueCollection section = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ProcessorsSetting/RunningProcessors");
            foreach (var key in section.AllKeys)
            {

                var typename = section[key].ToString();
                Type type = Type.GetType(typename);
                var _processor = _lifetimeScope.Resolve(type, new Parameter[]{
                                                            new NamedParameter("identifier", key)
                                                        });
                _Processors.Add(_processor as IBaseProcessor);
            }


            _workerTasks = _Processors.Select(p =>
            {
                return Task.Run((Func<Task>)p.Run);
            }).ToList();
        } 
        public MainService(ILog log, ILifetimeScope lifetimeScope)
        {
            _interval = 5 * 60 * 1000;
            _task = new Task(MainThread);
            _log = log;
            _lifetimeScope = lifetimeScope;
            _log.Info("Contructed");
        }

        private void MainThread()
        {
            while (!_docancel)
            {
                _log.Info("MainThread Started");
                System.Threading.Thread.Sleep(_interval);
            }
            _log.Info("End of MainTread");
        }

        public bool Start(HostControl hostControl)
        {
            this.BuildWorkerTasks();
            _task.Start();
            _log.Info("Service Started");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Service Stop");
            _docancel = true;
            return true;
        }
    }
}
