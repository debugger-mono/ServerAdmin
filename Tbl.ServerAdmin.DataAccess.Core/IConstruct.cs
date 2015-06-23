using System.Data;

namespace Tbl.ServerAdmin.DataAccess.Core
{
    public interface IConstruct<T>
    {
        T Construct(IDataReader reader, string columnPrefix = "");
    }
}