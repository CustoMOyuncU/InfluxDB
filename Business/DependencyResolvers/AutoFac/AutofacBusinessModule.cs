using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
  public class AutofacBusinessModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<TemperatureManager>().As<ITemperatureService>().SingleInstance();
      builder.RegisterType<InfluxDbDal>().As<IInfluxDb>().SingleInstance();

      //builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

      var assembly = System.Reflection.Assembly.GetExecutingAssembly();

      builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
          .EnableInterfaceInterceptors(new ProxyGenerationOptions()
          {
            Selector = new AspectInterceptorSelector()
          }).SingleInstance();
    }
  }
}
