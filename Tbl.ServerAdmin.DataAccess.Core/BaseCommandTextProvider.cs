
namespace Tbl.ServerAdmin.DataAccess.Core
{
    public abstract class BaseCommandTextProvider<TDBContext> : ICommandTextProvider<TDBContext> where TDBContext : DatabaseContext
    {
        protected readonly TDBContext dbContext;

        protected readonly ICommandDictionary commandDictionary;

        public BaseCommandTextProvider(TDBContext dbContext, ICommandDictionary commandDictionary)
        {
            this.dbContext = dbContext;
            this.commandDictionary = commandDictionary;
        }

        public virtual string GetCommandText(string commandIdentifier)
        {
            return this.commandDictionary.GetCommandsDictionary()[commandIdentifier];
        }
    }
}