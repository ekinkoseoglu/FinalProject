using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
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

       public IDataResult<List<Product>> GetAll()
        {
          
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed); // Ben 2DataResult<>' döndürüyorum, Çalıştığım tip 'List<Product>' dır, ilk parametre '_productDal.GetAll()' döndürdüğüm datadır, 'true' işlem sonucumdur ve mesajım da "Ürünler Listelendi"

        }

       public IDataResult<Product> GetById(int id)
       {
          
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id),Messages.HasShown);
       }

       public IDataResult<List<Product>> GetAllByCategoryId(int id)
       {
          

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id),Messages.ProductListed);
       }

       public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
       {
           

           return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >=min && p.UnitPrice <= max),Messages.ProductListed);
       }

       public IResult Add(Product product)
       {
           if (product.ProductName.Length < 2)
           {
               // Magic Strings
               return new ErrorResult(Messages.ProductNameInvalid);
           }
           _productDal.Add(product);
           return new Result(true, Messages.ProductAdded);
        }

       public IResult Update(Product product)
       {
           _productDal.Update(product);
           return new Result(true, "Ürün Güncellendi");
       }

       public IResult Delete(int id)
       {
           var deletedProduct = _productDal.Get(p => p.ProductId == id);
           _productDal.Delete(deletedProduct);
           return new Result(true, "Ürün Silindi");
        }

       public IDataResult<List<ProductDetailDto>> GetProductDetails()
       {
           
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductListed);
       }
   }
}
