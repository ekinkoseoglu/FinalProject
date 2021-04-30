using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context =new NorthwindContext())
            {
                // Yukarıda bir filtre verebilsin ama o filtre isterse vermeyedebilir(default fitle = null)
                return
                    filter == null
                        // NorthwindContext içindeki DbSet'teki Product için oraya yerleş (YANİ BEN Products TABLOSUYLA ÇALIŞACAĞIM DEMEK)  

                        ? context.Set<Product>().ToList() // Eğer filtre vermemişse (VERİTABANINDAKİ BÜTÜN Products TABLOSUNU LİSTEYE ÇEVİR VE ONU BANA RETURNLA) (ARKA PLANDA BİZİM İÇİN "Select * From Products" ÇALIŞTIRIYOR)


                        : context.Set<Product>().Where(filter).ToList(); //ama filtre vermişse o filtreyi uygula, ona göre data'yı listele

            }
        }

        public Product Get(Expression<Func<Product, bool>> filter) // Tek data getirecek
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public void Add(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext()) // "using" C#'a özel çok güzel bir yapıdır. Biz bir Class'ı new ledigimizde o bellekten Garbage Collector belli bir zamanda düzenli olarak gelir ve bellekten onu atar. Using içerisine yazdığımız nesneler using içerisine gelince anında Garbage Collector'e gelip "Beni bellekten at" der çünkü Context nesnesi biraz pahalı. Yani Northwind context bellekten işi bitince atılacak
            {
                var addedEntity = context.Entry(entity); // Git Veri Kaynağından benim parametre gönderdiğim productla bir tane nesne ekle (YANİ SADECE REFERANSI YAKALA- NORTHWIND context'e BAĞLA BU entity parametresini)


                addedEntity.State = EntityState.Added; // O ASLINDA EKLENECEK BİR NESNE

                context.SaveChanges(); // BURADAKİ BÜTÜN İŞLEMLERİ GERÇEKLEŞTİR

            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity); // Git Veri Kaynağından benim parametre gönderdiğim productla bir tane nesneyi eşleştirip GÜNCELLE (YANİ SADECE REFERANSI YAKALA - NORTHWIND context'e BAĞLA BU entity parametresini)


                updatedEntity.State = EntityState.Modified; // O ASLINDA GÜNCELLENECEK BİR NESNE

                context.SaveChanges(); // BURADAKİ BÜTÜN İŞLEMLERİ GERÇEKLEŞTİR
            }



        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity); // Git Veri Kaynağından benim parametre gönderdiğim productla bir tane nesneyi eşleştirip sil (YANİ SADECE REFERANSI YAKALA - NORTHWIND context'e BAĞLA BU entity parametresini)


                deletedEntity.State = EntityState.Deleted; // O ASLINDA SİLİNECEK BİR NESNE

                context.SaveChanges(); // BURADAKİ BÜTÜN İŞLEMLERİ GERÇEKLEŞTİR

            }
        }
    }
}
