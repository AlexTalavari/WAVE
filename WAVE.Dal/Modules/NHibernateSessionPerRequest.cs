using System;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using WAVE.Dal.Mappings;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate.Caches.SysCache;
//using NHibernate.Caches.SysCache2;

namespace WAVE.Dal.Modules
{
    public class NHibernateSessionPerRequest : IHttpModule
    {
        private static readonly ISessionFactory SessionFactory;

        // Constructs our HTTP module
        static NHibernateSessionPerRequest()
        {
            SessionFactory = CreateSessionFactory();
        }

        // Initializes the HTTP module
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        // Disposes the HTTP module
        public void Dispose() { }

        // Returns the current session
        public static ISession GetCurrentSession()
        {
            //SessionFactory.
            return SessionFactory.GetCurrentSession();
        }

        // Opens the session, begins the transaction, and binds the session
        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = SessionFactory.OpenSession();

            session.BeginTransaction();

            CurrentSessionContext.Bind(session);
        }

        // Unbinds the session, commits the transaction, and closes the session
        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);

            if (session == null) return;

            try
            {
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
                throw;
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }

        // Returns our session factory
        private static ISessionFactory CreateSessionFactory()
        {
            if (HttpContext.Current != null) //for the web apps
                _configFile = HttpContext.Current.Server.MapPath(
                                string.Format("~/App_Data/{0}", CacheFile)
                                );

            _configuration = LoadConfigurationFromFile();
            if (_configuration == null)
            {
                FluentlyConfigure();
                SaveConfigurationToFile(_configuration);
            }
            if (_configuration != null) return _configuration.BuildSessionFactory();
            return null;
        }

        

        // Returns our database configuration
        private static MsSqlConfiguration CreateDbConfigDebug2()
        {
            return MsSqlConfiguration
                .MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey("SQLAzureConnection"));
            //.ConnectionString(c => c.FromConnectionStringWithKey("SQLServer"));
        }

        // Updates the database schema if there are any changes to the model,
        // or drops and creates it if it doesn't exist
        private static void UpdateSchema(Configuration cfg)
        {
            new SchemaUpdate(cfg)
                .Execute(false, true);
        }
        private static void SaveConfigurationToFile(Configuration configuration)
        {
            using (var file = File.Open(_configFile, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(file, configuration);
            }
        }

        private static Configuration LoadConfigurationFromFile()
        {
            if (IsConfigurationFileValid == false)
                return null;
            try
            {
                using (var file = File.Open(_configFile, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as Configuration;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        private static void FluentlyConfigure()
        {
            if (_configuration == null)
            {
                _configuration = Fluently.Configure()
                .Database(CreateDbConfigDebug2)
                .CurrentSessionContext<WebSessionContext>()
                .Cache(c => c.ProviderClass<SysCacheProvider>().UseQueryCache())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>()
                    .Conventions.Add(DefaultCascade.All(), DefaultLazy.Always()))
                .ExposeConfiguration(UpdateSchema)
                .ExposeConfiguration(c => c.Properties.Add("cache.use_second_level_cache", "true"))
                .BuildConfiguration();
            }
        }
        private static bool IsConfigurationFileValid
        {
            get
            {
                var ass = Assembly.GetAssembly(typeof(UserMap));
                var configInfo = new FileInfo(_configFile);
                var assInfo = new FileInfo(ass.Location);
                return configInfo.LastWriteTime >= assInfo.LastWriteTime;
            }
        }

        private static Configuration _configuration;
        private static string _configFile;
        private const string CacheFile = "hibernate.cfg.xml";

    }
}