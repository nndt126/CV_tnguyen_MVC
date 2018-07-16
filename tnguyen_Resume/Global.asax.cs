using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using tnguyenResume.Bussiness.Model;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.DAL;
using System.Web.Helpers;
using System.Security.Claims;

namespace tnguyen_Resume
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<tnguyenResumeDbContext>().As<ItnguyenResumeDbContext>().InstancePerDependency();
            builder.RegisterType<WorkDAL>().As<IWorkDAL>().InstancePerDependency();
            builder.RegisterType<EducationDAL>().As<IEducationDAL>().InstancePerDependency();
            builder.RegisterType<InformationDAL>().As<IinformationDAL>().InstancePerDependency();
            builder.RegisterType<ProjectDAL>().As<IProjectDAL>().InstancePerDependency();
            builder.RegisterType<TestimonialDAL>().As<ITestimonialDAL>().InstancePerDependency();
            builder.RegisterType<SkilDAL>().As<ISkillDAL>().InstancePerDependency();
            builder.RegisterType<AccountDAL>().As<IAccountDAL>().InstancePerDependency();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}