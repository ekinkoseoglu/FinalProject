using Business.Concrete;
using System;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductDal InMemoryProductDal = new InMemoryProductDal();
            ProductManager productManagerIM = new ProductManager(InMemoryProductDal);
            //foreach (var x in productManagerIM.GetAll())
            //{
            //    Console.WriteLine($"Ürünün Adı: {x.ProductName},--- Ürünün CategoryId'si: {x.CategoryId},--- Ürünün Fiyatı {x.UnitPrice},--- Ürünün Stok adeti {x.UnitsInStock}");
                
            //}

            
            //InMemoryProductDal.Delete(new Product{ ProductId = 1, CategoryId = 1, ProductName = "Glass", UnitPrice = 15, UnitsInStock = 15 });

            //Console.WriteLine("----------------------------------------");

            //foreach (var x in productManagerIM.GetAll())
            //{
            //    Console.WriteLine($"Ürünün Adı: {x.ProductName},--- Ürünün CategoryId'si: {x.CategoryId},--- Ürünün Fiyatı {x.UnitPrice},--- Ürünün Stok adeti {x.UnitsInStock}");
            
            //}

            IProductDal efProductDal = new EfProductDal();
            ProductManager productManagerEf = new ProductManager(efProductDal);
            foreach (var x in productManagerEf.GetAll())
            {
                Console.WriteLine(x.ProductName);
            }

            foreach (var x in productManagerEf.GetAllByCategoryId(2))
            {
                Console.WriteLine(x.CategoryId +"  "+ x.ProductName);
            }
        }
    }
}
