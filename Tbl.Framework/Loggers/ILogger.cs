using System;

namespace Tbl.Framework.Loggers
{
    public interface ILogger
    {
        void Log(string message);

        void Log(Exception exeption);

        void Log(Exception exception, string message);
    }
}