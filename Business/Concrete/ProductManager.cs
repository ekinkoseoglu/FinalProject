using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
   public class ProductManager:IProductService
   {
       private IProductDal _productDal; // Ne InMemory İsmi geçecek ne EntityFramework ismi geçecek. Benim işim hepsinin referansını tutan İnterface'leriyle

       public ProductManager(IProductDal productDal)
       {
           _productDal = productDal;
       }

       public List<Product> GetAll()
        {
            // İş Kodları
            // Eğer şöyle şöyle olursa (Yetkisi var mı?) diyelim ki geçti.
            return _productDal.GetAll();

        }

       public List<Product> GetAllByCategoryId(int id)
       {
           return _productDal.GetAll(p=>p.CategoryId==id);
       }

       public List<Product> GetAllByUnitPrice(decimal min, decimal max)
       {
           return _productDal.GetAll(p => p.UnitPrice >=min && p.UnitPrice <= max);
       }

       public IResult Add(Product product)
       {
           _productDal.Add(product);
       }

       public IResult Update(Product product)
       {
           _productDal.Update(product);
           return new Result(true, "Ürün eklendi");
       }

       public IResult Delete(int id)
       {
           var deletedProduct = _productDal.Get(p => p.ProductId == id);
           _productDal.Delete(deletedProduct);
       }

       public List<ProductDetailDto> GetProductDetails()
       {
           return _productDal.GetProductDetails();
       }
   }
}
