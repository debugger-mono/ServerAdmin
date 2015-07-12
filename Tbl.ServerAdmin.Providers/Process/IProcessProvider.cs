using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tbl.ServerAdmin.Providers.Process
{
    public interface IProcessProvider
    {
        List<ProcessInfo> GetProcesses();
    }
}
