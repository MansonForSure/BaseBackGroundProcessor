using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackGroundJobs.Processors.Interfaces
{
    public interface IBaseProcessor
    {
        Task<int> Run();
    }
}
