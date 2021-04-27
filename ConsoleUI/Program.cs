using Business.Concrete;
using System;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductDal InMemoryProductDal = new InMemoryProductDal();
            ProductManager productManager = new ProductManager(InMemoryProductDal);
            foreach (var x in productManager.GetAll())
            {
                Console.WriteLine($"Ürünün Adı: {x.ProductName},--- Ürünün CategoryId'si: {x.CategoryId},--- Ürünün Fiyatı {x.UnitPrice},--- Ürünün Stok adeti {x.UnitsInStock}");
                
            }

            
            InMemoryProductDal.Delete(new Product{ ProductId = 1, CategoryId = 1, ProductName = "Glass", UnitPrice = 15, UnitsInStock = 15 });

            Console.WriteLine("----------------------------------------");

            foreach (var x in productManager.GetAll())
            {
                Console.WriteLine($"Ürünün Adı: {x.ProductName},--- Ürünün CategoryId'si: {x.CategoryId},--- Ürünün Fiyatı {x.UnitPrice},--- Ürünün Stok adeti {x.UnitsInStock}");

            }
        }
    }
}
