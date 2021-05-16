using Business.Concrete;
using System;
using Business.Abstract;
using Business.Constants;
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
            
            ProductManager productManagerIM = new ProductManager(new InMemoryProductDal());

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

            
          
            IProductService productManagerEf = new ProductManager(new EfProductDal());
            
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


            var result = productManagerEf.GetProductDetails();
            if (result.Success==true)
            {
                foreach (var x in productManagerEf.GetProductDetails().Data)
                {
                    Console.WriteLine(x.ProductName + "----" + x.CategoryName);
                }

                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            //CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            //Console.WriteLine("-----");
            //foreach (var x in customerManager.GetAll())
            //{
            //    Console.WriteLine(x.CompanyName+"----"+x.CustomerID+"----"+x.City);
            //}

            
        }
    }
}
