using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
  public interface ICacheManager
  {
      T Get<T>(string key); // Key değerine göre Method getir
      void Add(string key, object value, int duration); // Cache ye method ekle
      bool IsAdd(string key); // Bu method Cache eklendi mi daha önce?
      void Remove(string key); // Key Değerine göre Method sil
      void RemoveByPattern(string pattern); // RegExp Pattern ile belirlediğin pattern'e uyan bütün methodları seç
  }
}
