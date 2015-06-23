using Tbl.ServerAdmin.DataAccess.Models;

namespace Tbl.ServerAdmin.DataAccess.Handlers
{
    public interface IUserAccountHandler
    {
        int Add(UserAccount account);

        bool Validate(UserAccount account);
    }
}
