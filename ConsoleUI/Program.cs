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

            // 05/11/2021 9. Ders

            CategoryManager categoryManagerEf = new CategoryManager(new EfCategoryDal());
            CustomerManager customerManagerEf = new CustomerManager(new EfCustomerDal());
            




            //CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            //Console.WriteLine("-----");
            //foreach (var x in customerManager.GetAll())
            //{
            //    Console.WriteLine(x.CompanyName+"----"+x.CustomerID+"----"+x.City);
            //}


            // 05/16/2021 10 Ders

            var result = productManagerEf.GetProductDetails(); // Product Listele ve Mesaj ver
            if (result.Success == true)
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

            Console.WriteLine("---------------");
            var result2 = categoryManagerEf.GetAll(); // Category Listele ve Mesaj ver
            if (result2.Success == true)
            {
                foreach (var x in result2.Data)
                {
                    Console.WriteLine(x.CategoryId +"-----"+x.CategoryName);
                }

                Console.WriteLine(result2.Message);

            }
            else
            {
                Console.WriteLine(result2.Message);
            }

            Console.WriteLine("----------");

            var result3 = customerManagerEf.GetAll(); // Customer Listele ve Mesaj ver
            if (result3.Success==true)
            {
                foreach (var x in result3.Data)
                {
                    Console.WriteLine(x.CompanyName+"---"+x.CustomerID);
                }

                Console.WriteLine(result3.Message);
            }
            else
            {
                Console.WriteLine(result3.Message);
            }
        }

    }
}
