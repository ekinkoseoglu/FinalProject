using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;


namespace Core.DataAccess
{
    // Generic Constraint
    // class: Sadece classlar olabilir demek değil, REFERANS TİPLER OLABİLİR demek (int,bool,decimal gibi değer tipler gitti yani)
    // IEntity: IEntity olabilir ya da IEntity'den implemente olan bir nesne olabilir
    // new(): New'lenebilir olmalı (IEntity'i saf dışı bıraktık aksdjh)
    public interface IEntityRepository<T> where T:class,IEntity, new() // T bir referans tip olmalı ve T ya IEntity olabilir ya da IEntity'den implemente olan bir nesne olabilir ama new yaptığımız için de  New'lenebilir olmalı (IEntity'i saf dışı bıraktık aksdjh)
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null); // Ürünleri spesifik özelliklerine göre filtreleyebilmek için tek tek methot yazmak yerine Expression kullanarak daha kolay filtreli listeleme yapabiliyoruz
        T Get(Expression<Func<T, bool>> filter); // Genelde tek bir data getirmek için (Spesifik olarak tıklanılan verinin detaylarına girebilmek için yaratılan field)
        void Add(T entity); // Interface metotları Default publictir (Kendi degil ama) 
        void Update(T entity);
        void Delete(T entity);
      

    }
}
