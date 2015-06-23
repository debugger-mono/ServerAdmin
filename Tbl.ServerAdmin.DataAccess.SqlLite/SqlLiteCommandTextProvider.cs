using Tbl.ServerAdmin.DataAccess.Core;

namespace Tbl.ServerAdmin.DataAccess.SqlLite
{
    public class SqlLiteCommandTextProvider<TDBContext> : BaseCommandTextProvider<TDBContext> where TDBContext : DatabaseContext
    {

        public SqlLiteCommandTextProvider(TDBContext dbContext, ICommandDictionary commandDictionary) :
            base(dbContext, commandDictionary)
        {

        }
    }
}
