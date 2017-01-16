using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackGroundJobs.Processors.Interfaces;
using Common.Logging;

namespace BackGroundJobs.Processors.Base
{
    public class BaseProcessor : IBaseProcessor
    {
        protected readonly ILog _log;
        protected readonly string _identifier;
        public BaseProcessor(ILog log, string identifier)
        {

            _log = log;
            //_identifier = Guid.NewGuid().ToString();
            _identifier = identifier;
            _log.Info(string.Format("BaseProcessor Created: {0}", _identifier));
        }

        public virtual Task<int> Run()
        {
            _log.Error("Not Implemented Yet");
            throw new NotImplementedException();
        }

    }
}
