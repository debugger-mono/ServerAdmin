using System.Collections.Generic;

namespace Tbl.ServerAdmin.DataAccess.Core
{
    public interface ICommandDictionary
    {
        Dictionary<string, string> GetCommandsDictionary();
    }
}