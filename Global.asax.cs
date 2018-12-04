using AutoMapper;
using ReforgedApi.Configuration.Handlers;
using ReforgedApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ReforgedApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiKeyHandler());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.countryCode,
                    opts => opts.MapFrom(src => src.Country.code))
                .ForMember(dest => dest.favoriteFactionImg,
                    opts => opts.MapFrom(src => src.favoriteFaction.img))
                .ForMember(dest => dest.scores,
                    opts => opts.MapFrom(src => src.Scores)));
        }
    }
}
