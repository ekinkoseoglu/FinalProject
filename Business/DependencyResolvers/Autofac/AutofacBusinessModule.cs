using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); // Birisi senden "IProductService" isterse ona "ProductManager" ver.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance(); // Birisi senden "IProductDal" isterse ona "EfProductDal" ver.


            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // Çalışan uygulama içerisinde (1)

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //Yukarıda IoC Container ile implemente edilmiş interfaceleri bul (2)
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() //Autofac Onlar için "Git bak bak bakalım bunların Aspectleri var mı kontrol et (Method üstündekiş köşeli parantezli yapı) " (3)
                }).SingleInstance();
        }
    }
}
