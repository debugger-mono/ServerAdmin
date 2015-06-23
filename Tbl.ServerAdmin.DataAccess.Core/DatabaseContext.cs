namespace Tbl.ServerAdmin.DataAccess.Core
{
    public abstract class DatabaseContext
    {
        public string ConnectionStringKey { get; set; }

        public string ProviderName { get; set; }
    }
}