using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;


namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product> // Sen bir IEntityRepository'sin ve çalışma tipin "Product"'dır dedik
    {
        // Buraya ürüne ait özel operasyonları koyacağız

        // Örneğin ürünün detaylarını getirmek için ürün Category tablolarına join atmak gibi

        // DTO metodları buraya yazılacak 
        List<ProductDetailDto> GetProductDetails();

    }
}
//Code Refactoring