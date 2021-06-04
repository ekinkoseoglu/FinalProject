using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Business.Abstract;
using Business.Concrete;
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
        }
    }
}
