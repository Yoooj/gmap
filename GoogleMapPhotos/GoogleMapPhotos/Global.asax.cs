using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using GoogleMapPhotos.Models.CF;
using GoogleMapPhotos.Models.PhotoModel;
using GoogleMapPhotos.Models.ProfileModel;
using GoogleMapPhotos.Models.TripModel;
using TripGMap.Models.PhotoModel;
using TripGMap.Models.ProfileModel;

namespace GoogleMapPhotos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoFac_Configuration();
        }

        protected void AutoFac_Configuration()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CfTripRepository>()
                .As<ITripRepository>().InstancePerRequest();

            builder.RegisterType<CfPhotoRepository>()
                .As<IPhotoRepository>().InstancePerRequest();

            builder.RegisterType<CfProfileRepository>()
                .As<IProfileRepository>().InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
