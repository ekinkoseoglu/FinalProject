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

            // 05/2/2021 7. Ders
            /*
            foreach (var x in productManagerIM.GetAll())
            {
                Console.WriteLine($"Ürünün Adı: {x.ProductName},--- Ürünün CategoryId'si: {x.CategoryId},--- Ürünün Fiyatı {x.UnitPrice},--- Ürünün Stok adeti {x.UnitsInStock}");

            }


            InMemoryProductDal.Delete(new Product { ProductId = 1, CategoryId = 1, ProductName = "Glass", UnitPrice = 15, UnitsInStock = 15 });

            Console.WriteLine("----------------------------------------");

            foreach (var x in productManagerIM.GetAll())
            {
                Console.WriteLine($"Ürünün Adı: {x.ProductName},--- Ürünün CategoryId'si: {x.CategoryId},--- Ürünün Fiyatı {x.UnitPrice},--- Ürünün Stok adeti {x.UnitsInStock}");

            } 
            */

            // 05/9/2021 8. Ders 

            
          
            ProductManager productManager = new ProductManager(new EfProductDal());
            
            /*
            foreach (var x in productManager.GetAll())
            {
                Console.WriteLine(x.ProductName);
            }

            foreach (var x in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(x.CategoryId +"  "+ x.ProductName);
                
            }

            foreach (var x in productManager.GetAllByUnitPrice(20,50))
            {
                Console.WriteLine(x.ProductName + " " + x.UnitPrice);
            }

            Console.WriteLine(productManager.GetAllByUnitPrice(20, 40).Count);
            */

            // 05/11/2021 9, Ders

            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var x in productManager.GetProductDetails())
            {
                Console.WriteLine(x.ProductName + "---" + x.CategoryName);
            }

            
        }
    }
}
