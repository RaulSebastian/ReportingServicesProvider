using Funq;
using ServiceStack;
using ReportingServicesProvider.ServiceInterface;
using ServiceStack.Api.Swagger;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

//using ServiceStack.OrmLite;
//using ServiceStack.OrmLite.SqlServer;

namespace ReportingServicesProvider
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("ReportingServicesProvider", typeof(ServersService).Assembly)
        {
            JsConfig.TreatEnumAsInteger = true;
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //TODO: add documentation for used features
            Plugins.Add(new SwaggerFeature());
            Plugins.Add(new PostmanFeature());

            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(
                    @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ReportingServicesProvider;Integrated Security=True;TrustServerCertificate=True",
                    SqlServer2012Dialect.Provider));

            //container.Register<IDbConnectionFactory>(c =>
            //    new OrmLiteConnectionFactory(":memory:", SqlliteDialect.Provider));
        }
    }
}