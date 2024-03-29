﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)


        {
            /*         IoC Container            */

            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); // Birisi senden "IProductService" isterse ona "ProductManager" ver.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance(); // Birisi senden "IProductDal" isterse ona "EfProductDal" ver.

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            


            /*             Aspect Interceptor Selector                    */

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // Çalışan uygulama içerisinde (1)

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //Yukarıda IoC Container ile implemente edilmiş interfaceleri bul (2)
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() //Autofac Onlar için "Git bak bak bakalım bunların Aspectleri var mı kontrol et (Method üstündekiş köşeli parantezli yapı) " (3)
                }).SingleInstance();
        }
    }
}
