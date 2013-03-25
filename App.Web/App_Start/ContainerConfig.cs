using App.Core.Services;
using App.Web.Code.Unity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web
{
    public static class ContainerConfig
    {
        public static void RegisterTypes(UnityContainer container)
        {
            container.RegisterInstance<IUnityContainer>(container);

            container.RegisterType<IWeatherService, WundergroundWeatherService>();

            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }
    }
}