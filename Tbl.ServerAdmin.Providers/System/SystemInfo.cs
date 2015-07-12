using System.Collections.Generic;
using Tbl.ServerAdmin.Providers.Os;

namespace Tbl.ServerAdmin.Providers.System
{
    public class SystemInfo
    {
        public string MachineName { get; set; }

        public OsInfo OperationSystem { get; set; }

        public CpuInfo Cpu { get; set; }

        public int TotalMemory { get; set; }

        public int FreeMemory { get; set; }

        public List<NetworkAddress> NetworkAddresses { get; set; }
    }
}