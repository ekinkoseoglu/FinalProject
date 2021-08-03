using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule // Framework katmanımız ve bizim tüm projelerimizde ve hatta şirket değiştirsek orada da kullanabileceğimiz kodları içeren yapıdır.
    {
        void Load(IServiceCollection serviceCollection); // Injection edeceğimiz serviceleri IServiceCollection yükleyecek
    }
}
