using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal; // Ne InMemory İsmi geçecek ne EntityFramework ismi geçecek. Benim işim hepsinin referansını tutan İnterface'leriyle

        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed); // Ben 2DataResult<>' döndürüyorum, Çalıştığım tip 'List<Product>' dır, ilk parametre '_productDal.GetAll()' döndürdüğüm datadır, 'true' işlem sonucumdur ve mesajım da "Ürünler Listelendi"

        }

        public IDataResult<Product> GetById(int id)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<Product>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id), Messages.HasShown);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {

            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {


            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductListed);
        }


        [ValidationAspect(typeof(ProductValidator))] // ProductValidator kullanarak Product parametreli bu metodu kontrol et
        public IResult Add(Product product)
        {
            // Business CODES 

            var ErrorRule = BusinessRules.Run(CheckProductCategoryLimit(product.CategoryId), CheckProductName(product), CheckGeneralCategoryLimit());

            if (ErrorRule != null) // Eğer kurala uymayan bir durum varsa
            {
                return ErrorRule; // O Methodu döndür ErrorMessage'si zaten onun içerisinde var 
            }


            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);


        }

        public IResult Update(Product product)
        {

            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _productDal.Update(product);
            return new SuccessResult("Ürün Güncellendi");
        }

        public IResult Delete(int id)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }

            var deletedProduct = _productDal.Get(p => p.ProductId == id);
            _productDal.Delete(deletedProduct);
            return new SuccessResult("Ürün Silindi");
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductListed);
        }




        /* BUSINESS RULES  */


        private IResult CheckProductCategoryLimit(int categoryId)
        {
            // Select Count(*) from Products where CategoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.CategoryLimitError);
            }

            return new SuccessResult();
        }

        private IResult CheckProductName(Product product)
        {
            var result = _productDal.GetAll(p => p.ProductName == product.ProductName);
            if (result.Count > 0)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckGeneralCategoryLimit()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.GeneralCategoryLimit);
            }

            return new SuccessResult();
        }
    }
}
