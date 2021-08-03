using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection,
            ICoreModule[] modules) // İstediğim kadar Resolver'i yani ICoreModule olan şeyleri buraya ekleyebileyim. (Polymorphism)
        {
            foreach (var module in modules) // Bize eklenen her bir module için modül'ü yükle 
            {
                module.Load(serviceCollection); // Birden fazla modülü de IServiceCollection koleksiyonuma ekleyebileceğimi gösteriyor.
            }

            return ServiceTool.Create(serviceCollection); // O serviceleri Provide Et
        }
    }
}
