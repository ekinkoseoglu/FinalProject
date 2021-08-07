using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule:ICoreModule 
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache(); // built-in IMemoryCache'nin instancesi (IMemoryCache interfacesinin lokalde çalışabilmesi için) (Built-in cacheyi çalıştırmamız için bu injection'u yazmalıyız, başka bir zaman Redis'e geçersem bunu yazmama gerek yok.)
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
