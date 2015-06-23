using Autofac;
using ServerAdmin.Provider;
using Tbl.ServerAdmin.DataAccess;
using Tbl.ServerAdmin.DataAccess.Commands;
using Tbl.ServerAdmin.DataAccess.Core;
using Tbl.ServerAdmin.DataAccess.Handlers;
using Tbl.ServerAdmin.DataAccess.SqlLite;

namespace ServerAdmin.App_Start
{
    public static class AutofacConfig
    {
        private static ContainerBuilder builder;

        public static IContainer SetUp()
        {
            InitializeContainer();
            RegisterDependencies();
            return BuildAndResolve();
        }

        private static void InitializeContainer()
        {
            builder = new ContainerBuilder();
        }

        private static void RegisterDependencies()
        {
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterOwinApplicationContainer();

            builder.Register<ServerAdminDbContext>(t =>
                {
                    ServerAdminDbContext context = new ServerAdminDbContext()
                    {
                        ProviderName = "Mono.Data.SqlLite"
                    };
                    return context;
                });

            builder.RegisterType<SqlLiteCommandTextProvider<ServerAdminDbContext>>().As<ICommandTextProvider<ServerAdminDbContext>>();

            builder.RegisterType<SqlLiteDataAccessHandler<ServerAdminDbContext>>().As<IDataAccessHandler<ServerAdminDbContext>>();

            builder.RegisterType<SqlLiteCommandDictionary>().As<ICommandDictionary>();

            builder.RegisterType<UserAccountHandler>().As<IUserAccountHandler>();

            builder.Register<InternalAuthorisationProvider>(t =>
            {
                IDataAccessHandler<ServerAdminDbContext> dataAccess = t.Resolve<IDataAccessHandler<ServerAdminDbContext>>();
                IUserAccountHandler userAccountHandler = t.Resolve<IUserAccountHandler>();
                return new InternalAuthorisationProvider(dataAccess, userAccountHandler);
            });
        }

        private static IContainer BuildAndResolve()
        {
            IContainer container = builder.Build();
            return container;
        }
    }
}