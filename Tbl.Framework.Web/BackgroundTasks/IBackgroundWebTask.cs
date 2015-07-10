using System;
using System.Threading;
using System.Web.Hosting;

namespace Tbl.Framework.Web.BackgroundTasks
{
    public interface IBackgroundWebTask : IRegisteredObject
    {
        CancellationToken CancelToken { get; }

        void RunTask(Action action);
    }
}