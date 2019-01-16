using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Dal.Repositories;
using WAVE.Website.App_Start;
using WAVE.Website.Helpers.DTOs;
using WebActivatorEx;
using Action = WAVE.Dal.Entities.Action;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace WAVE.Website.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRepository<User>>().To<Repository<User>>().InRequestScope();
            kernel.Bind<IRepository<UserDto>>().To<Repository<UserDto>>().InRequestScope();
            kernel.Bind<IRepository<Action>>().To<Repository<Action>>().InRequestScope();
            kernel.Bind<IRepository<HomePageDto>>().To<Repository<HomePageDto>>().InRequestScope();
            kernel.Bind<IRepository<Volunteer>>().To<Repository<Volunteer>>().InRequestScope();
            kernel.Bind<IRepository<Category>>().To<Repository<Category>>().InRequestScope();
            kernel.Bind<IRepository<ActionUpdate>>().To<Repository<ActionUpdate>>().InRequestScope();
            kernel.Bind<IRepository<Comment>>().To<Repository<Comment>>().InRequestScope();
            kernel.Bind<IRepository<ImageData>>().To<Repository<ImageData>>().InRequestScope();
            kernel.Bind<IRepository<Unite>>().To<Repository<Unite>>().InRequestScope();
            kernel.Bind<IRepository<BlogPost>>().To<Repository<BlogPost>>().InRequestScope();
            kernel.Bind<IRepository<Team>>().To<Repository<Team>>().InRequestScope();
            kernel.Bind<IRepository<RestrictedUserNames>>().To<Repository<RestrictedUserNames>>().InRequestScope();
            kernel.Bind<IRepository<UserMessage>>().To<Repository<UserMessage>>().InRequestScope();
        }
    }
}