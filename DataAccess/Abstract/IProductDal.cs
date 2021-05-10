using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;


namespace DataAccess.Abstract
{
   public interface IProductDal:IEntityRepository<Product> // Sen bir IEntityRepository'sin ve çalışma tipin "Product"'dır dedik
   {
      // Buraya ürüne ait özel operasyonları koyacağız
      
      // Örneğin ürünün detaylarını getirmek için ürün Category tablolarına join atmak gibi

      // DTO metodları buraya yazılacak 

   }
}
//Code Refactoring