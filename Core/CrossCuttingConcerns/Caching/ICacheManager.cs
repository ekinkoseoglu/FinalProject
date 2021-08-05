namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager // Bu interface ilerde değiştirmek isteyeceğim bütün cachelerimin (build-in, Redis, Lockstash) alternatifi olacak ve IoC üzerinden dependencysini değiştirmem yeterli olacak. 
    {
        T Get<T>(string key); // Key değerine göre Method getir
        object Get(string key);// Generic olmadan Key değerine göre getir.
        void Add(string key, object value, int duration/*Cachede ne kadar duracak*/); // Cache ye method ekle
        bool IsAdd(string key); // Bu method Cache eklendi mi daha önce?
        void Remove(string key); // Key Değerine göre Method sil
        void RemoveByPattern(string pattern); // RegExp Pattern ile belirlediğin pattern'e uyan bütün methodları seç (içinde "Get" olan bütün metodları sil)
    }
}
