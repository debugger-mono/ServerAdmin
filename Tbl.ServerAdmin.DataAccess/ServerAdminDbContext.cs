using Tbl.ServerAdmin.DataAccess.Core;

namespace Tbl.ServerAdmin.DataAccess
{
    public class ServerAdminDbContext : DatabaseContext
    {
        public ServerAdminDbContext()
        {
            this.ConnectionStringKey = "ServerAdminDb";
        }
    }
}
