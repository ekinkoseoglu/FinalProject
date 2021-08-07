using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);

        IResult Add(Product product); // sen void döndürme sen IResult döndür
        IResult Update(Product product);
        IResult Delete(int id);

        IResult AddTransactionalTest(Product product);
        IDataResult<List<ProductDetailDto>> GetProductDetails();

    }
}
