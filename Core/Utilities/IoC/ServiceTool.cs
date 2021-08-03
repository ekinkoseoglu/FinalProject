using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Utilities.IoC
{
    public static class ServiceTool //Aspectler'in Injection altyapımızı okuyabilmesine yarayan araç olacak 

    // WebAPIde veya Autofacde oluşturduğumuz injenctionları oluşturabilmemize yarıyor
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) // .Net içerisindeki servicelerini (IServiceCollection) al ve onları kendin build et
        {
            ServiceProvider = services.BuildServiceProvider();// Artık bu somut Container içinde IServiceCollection'dan aldığımız Injectionlar var
                                                              // ServiceProvider.GetService<> dediğimde içeride Generic olarak bildirdiğim türe karşılık gelen Nesne(class) hangisiyse bana onu döndürecek.


            return services;
        }
    }
}
