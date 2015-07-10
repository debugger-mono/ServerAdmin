using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using Tbl.Framework.Loggers;

namespace Tbl.Framework.Web.BackgroundTasks
{
    public sealed class BackgroundWebTask : IBackgroundWebTask
    {
        private readonly ILogger logger;
        private readonly CancellationTokenSource cancelSource;
        private Task work;

        public BackgroundWebTask(ILogger logger)
        {
            this.logger = logger;
            this.cancelSource = new CancellationTokenSource();
            HostingEnvironment.RegisterObject(this);
        }

        #region IBackgroundWebTask Members
        public CancellationToken CancelToken
        {
            get { return this.cancelSource.Token; }
        }

        public void RunTask(Action action)
        {
            this.work = Task.Factory.StartNew(action);
        }

        public void Stop(bool immediate)
        {
            if (this.work != null && !(this.work.IsCanceled || this.work.IsCompleted || this.work.IsFaulted))
            {
                this.cancelSource.Cancel(false);
            }

            if (this.work != null && !(this.work.IsCanceled || this.work.IsCompleted || this.work.IsFaulted))
            {
                try
                {
                    this.logger.Log(string.Format("{0} ({1}) - Waiting for Task to finish. Immediate : {2}", this.GetType().Name, this.GetHashCode(), immediate));
                    this.work.Wait();
                    this.logger.Log(string.Format("{0} ({1}) - Task finished successfully. Immediate : {2}", this.GetType().Name, this.GetHashCode(), immediate));
                }
                catch (Exception ex)
                {
                    this.logger.Log(ex, string.Format("{0} ({1}) - Exception Caught on Task.Wait(). Immediate : {2}", this.GetType().Name, this.GetHashCode(), immediate));
                }
                finally
                {
                    this.cancelSource.Dispose();
                }
            }
        }
        #endregion
    }
}