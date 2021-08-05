using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        // Adapter Pattern
        private IMemoryCache _memoryCache; // Microsoft'un kendi interfacesi (Microsoft.Extensions.Caching.Memory;)

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); // IoC Containerda(CoreModule) serviceCollection.AddMemoryCache(); sayesinde localdeki IMemoryCache'nin instancesi yaratıldı. Şimdi ServiceTool yardımı ile o instanceye ulaşıyoruz ki burada kullanalım.
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration)/*duration kaç dakikaysa o  expiration süresi*/);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); // Böyle bir değer cachede var mı yok mu? 
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern) // Çalışma anında belirli bir pattern'e uyanları bellekten silmeye yarıyor
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance); // Git bellekteki "EntriesCollection" u bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic; // Definiion'u "_memoryCache" olanları bul
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection) // Bulduğun her bir cache elemanını gez
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null); // O cache'de yakaladığın metodları al ve.... 
                cacheCollectionValues.Add(cacheItemValue); // Yukarıda yarattığım CacheCollectionValues Listesine ekle 
            }

            var regex = new Regex(pattern, RegexOptions.Singleline /*Single line olacak*/| RegexOptions.Compiled /*Compile edilmiş olacak*/| RegexOptions.IgnoreCase/*Case sensitive olmayacak*/);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); // Yukarıdaki "cacheCollectionValues" listesine eklediğimiz cache datalarının anahtarlarından benim gönderdiğime uygun olanlar varsa onları "keysToRemove" içine atacak. //   

            foreach (var key in keysToRemove) // Ve "keysToRemove" u tek tek gezerek içindeki cache datalarını silecek. (Uyanların keylerini buluyorum)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
