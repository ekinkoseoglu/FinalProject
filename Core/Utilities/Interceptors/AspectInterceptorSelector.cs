using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList(); // Class'ın Attributelerini Oku
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true); //ve Methodun Attributelerini Oku ve onları bir listeye koy
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); // Otomatik olarak sistemdeki bütün methodları Log'a dahil et.

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        } // Yanlıs onların çalışmasını da Öncelik değerine göre Sırala
    }
}
