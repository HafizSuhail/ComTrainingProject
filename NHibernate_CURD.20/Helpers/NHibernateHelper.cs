using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using ISession = NHibernate.ISession;
using NHibernate_CURD._20.BusinessEntities;
using System.Reflection;
using NHibernate.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace NHibernate_CURD._20.Helpers
{
    public static class NHibernateHelper
    {
        private static readonly ISessionFactory _sessionFactory;

        public static void ConfigureServices(IServiceCollection services)
        {
            // Configure NHibernate
            var sessionFactory = CreateSessionFactory();
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped(factory => sessionFactory.OpenStatelessSession());

            // Other services and configurations...
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Server=DESKTOP-FP28SOE;Database=Microcare;User Id=Muhammed;Password=suhail@123"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Patients>()
                .AddFromAssemblyOf<Hospital>()
                .AddFromAssemblyOf<City>())
                .BuildSessionFactory();
        }

       
    }
}
