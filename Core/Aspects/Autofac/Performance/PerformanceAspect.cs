using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch; // Bir tane timer koyuyorum

        public PerformanceAspect(int interval) // İnterval metodun çalıma süresi sınırıdır. Aspect olarak yazarken kaç saniye koyarsak o saniyeyi geçtiği zaman beni uyarır.
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>(); // CoreModule de implementasyonu yapıldığı için ServiceTool ile çağırıyorum.
        }


        protected override void OnBefore(IInvocation invocation) // Methodun önünde kronometreyi başlatıyorum
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval) // O ana kadarki geçen süreyi hesaplıyorum. Geçen süre girdiğim saniyeyi geçmişse
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}"); // Console'a log olarak yazmışım.(Hangi method kaç saniye sürdü çalışması onu gösterecek)
            }
            _stopwatch.Reset();
        }
    }
}
