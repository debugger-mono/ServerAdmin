using System.Data;

namespace ServerAdmin.DataAccess
{
    public interface IConstruct<T>
    {
        T Construct(IDataReader reader, string columnPrefix = "");
    }
}
