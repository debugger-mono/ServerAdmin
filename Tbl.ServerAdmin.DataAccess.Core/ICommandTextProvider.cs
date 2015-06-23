namespace Tbl.ServerAdmin.DataAccess.Core
{
    public interface ICommandTextProvider<TDBContext> where TDBContext : DatabaseContext
    {
        string GetCommandText(string commandIdentifier);
    }
}
