using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)// Biz duration girmezsek 60 dakika içinde kendisini Cache den silecek
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // (MetodunNamespacasi+Interfacesi).(Methodunismi)
            var arguments = invocation.Arguments.ToList(); // Methodun parametrelerini listeye çevir
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>")/*yazılan parametrelerin her biri için aralarına virgül (,) koy*/)})"; // Methodda eğer parametre değeri varsa tek tek ekliyorum
            if (_cacheManager.IsAdd(key)) // Cache bellekte böyle bir cache anahtarı var mı kontrol et
            {
                invocation.ReturnValue = _cacheManager.Get(key); // Bu methodun return değeri Controldeki methodundan return edilmesin de onun return'u Cachedeki data olsun demek. Böylece Controller'a hiç girmiyor, direkt Cacheden getiriyor.
                return;
            }
            invocation.Proceed(); // Ama yoksa invocation'u Çalıştır, devam ettir.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // Anahtarı, Return value'yi , Duration'u Cache'e ekle
        }
    }
}
