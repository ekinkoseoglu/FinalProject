using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> // Bana birtane tablo ver bir tane de context tipi ver
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()

    {
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // Yukarıda bir filtre verebilsin ama o filtre isterse vermeyedebilir(default fitle = null)
                return filter == null
                        // NorthwindContext içindeki DbSet'teki Product için oraya yerleş (YANİ BEN Products TABLOSUYLA ÇALIŞACAĞIM DEMEK)  

                        ? context.Set<TEntity>().ToList() // Eğer filtre vermemişse (VERİTABANINDAKİ BÜTÜN Products TABLOSUNU LİSTEYE ÇEVİR VE ONU BANA RETURNLA) (ARKA PLANDA BİZİM İÇİN "Select * From Products" ÇALIŞTIRIYOR)


                        : context.Set<TEntity>().Where(filter).ToList(); //ama filtre vermişse o filtreyi uygula, ona göre data'yı listele

            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter) // Tek data getirecek
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public void Add(TEntity entity)
        {   // IDisposable Pattern Implementation of C#
            using (TContext context = new TContext()) // "using" C#'a özel çok güzel bir yapıdır. Biz bir Class'ı new ledigimizde o bellekten Garbage Collector belli bir zamanda düzenli olarak gelir ve bellekten onu atar. Using içerisine yazdığımız nesneler using içerisine gelince anında Garbage Collector'e gelip "Beni bellekten at" der çünkü Context nesnesi biraz pahalı. Yani Northwind context bellekten işi bitince atılacak
            {
                var addedEntity = context.Entry(entity); // Git Veri Kaynağından benim parametre gönderdiğim productla bir tane nesne ekle (YANİ SADECE REFERANSI YAKALA- NORTHWIND context'e BAĞLA BU entity parametresini)


                addedEntity.State = EntityState.Added; // O ASLINDA EKLENECEK BİR NESNE

                context.SaveChanges(); // BURADAKİ BÜTÜN İŞLEMLERİ GERÇEKLEŞTİR

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity); // Git Veri Kaynağından benim parametre gönderdiğim productla bir tane nesneyi eşleştirip GÜNCELLE (YANİ SADECE REFERANSI YAKALA - NORTHWIND context'e BAĞLA BU entity parametresini)


                updatedEntity.State = EntityState.Modified; // O ASLINDA GÜNCELLENECEK BİR NESNE

                context.SaveChanges(); // BURADAKİ BÜTÜN İŞLEMLERİ GERÇEKLEŞTİR
            }



        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); // Git Veri Kaynağından benim parametre gönderdiğim productla bir tane nesneyi eşleştirip sil (YANİ SADECE REFERANSI YAKALA - NORTHWIND context'e BAĞLA BU entity parametresini)


                deletedEntity.State = EntityState.Deleted; // O ASLINDA SİLİNECEK BİR NESNE

                context.SaveChanges(); // BURADAKİ BÜTÜN İŞLEMLERİ GERÇEKLEŞTİR

            }
        }
    }
}
