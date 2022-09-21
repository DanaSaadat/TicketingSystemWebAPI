[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TicketingSystemNew2.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TicketingSystemNew2.App_Start.NinjectWebCommon), "Stop")]

namespace TicketingSystemNew2.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Service;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
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
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDepartmentService>().To<DepartmentService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IPermissionService>().To<PermissionService>().InRequestScope();
            kernel.Bind<IPermissionUserService>().To<PermissionUserService>().InRequestScope(); 
            kernel.Bind<IProjectService>().To<ProjectService>().InRequestScope(); 
            kernel.Bind<IProjectClientService>().To<ProjectClientService>().InRequestScope(); 
            kernel.Bind<IProjectEmpService>().To<ProjectEmpService>().InRequestScope(); 
            kernel.Bind<ITicketService>().To<TicketService>().InRequestScope(); 
            kernel.Bind<ILoginService>().To<LoginService>().InRequestScope(); 
            kernel.Bind<IAuditService>().To<AuditService>().InRequestScope(); 


        }
    }
}
