using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BackGroundJobs.Processors.Base;
using BackGroundJobs.Processors.Interfaces;
using Common.Logging;

namespace BackGroundJobs.Processors
{
    public class TestProcessor:BaseProcessor
    {
        public override async Task<int> Run()
        {
            _log.Info("Test Processor Sucessfully Called -- All the things setup correctly!");
            return 1;
        }

        public TestProcessor(ILog log, string identifier) : base(log, identifier)
        {

        }
    }
}
