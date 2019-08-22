using Autofac;
using Lib_DynamicConfiguration.DataAccess.Cache;
using Lib_DynamicConfiguration.DataAccess.Database;

namespace Lib_DynamicConfiguration.Infrastructure
{
    public class IoC
    {
        public static ContainerBuilder builder;
        private static IContainer container;

        static IoC()
        {
            if (builder == null)
            {
                builder = new ContainerBuilder();
                builder.RegisterType<MongoDbContext>().As<IMongoDbContext>()
                    .WithParameter(new NamedParameter("connectionString", ConfigurationReader._connectionString)).SingleInstance();
                builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>().SingleInstance();
                builder.RegisterType<CacheRepository>().As<ICacheRepository>().SingleInstance();
            }
        }


        public static IContainer CreateContainer()
        {
            container = builder.Build();
            return container;
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
