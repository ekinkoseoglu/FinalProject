using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    // Context: DB Tabloları ile proje classlarını bağlamak
    public class NorthwindContext: DbContext // EntityFramework kurmam ile beraber "DbContext" isimli base bir sınıf geliyor ve bu aslında bizim Context'imizin ta kendisi
    {
        // Bu methot benim projem hangi veritabanı ile ilişkiliyi belirteceğim yer
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Sql server kullanacağız o zaman Sql server'a nasıl bağlanacagını belirtmem yeterli
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true;");
        }

        public DbSet<Product> Products { get; set; } // Benim "Product" Class'ımı veritabanındaki "Products" tablosuna bağla
        public DbSet<Category> Categories { get; set; }// Benim "Category" Class'ımı veritabanındaki "Categories" tablosuna bağla
        public DbSet<Customer> Customers { get; set; }// Benim "Customer" Class'ımı veritabanındaki "Customers" tablosuna bağla
        public DbSet<Order> Orders { get; set; }
    }
}
