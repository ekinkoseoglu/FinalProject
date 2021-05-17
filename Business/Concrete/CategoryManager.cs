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

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<List<Category>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<Category> GetById(int cId) // select * from Categories where CategoryId = ???
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorDataResult<Category>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == cId), Messages.HasShown); // CATEGORY SERVİCE YAPTIK İÇİNİ İMZALARLA YAZDIK SUAN MANAGER'İNİ YAZIYORUZ
        }

        public IResult Add(Category category)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _categoryDal.Add(category);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Update(Category category)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _categoryDal.Update(category);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(int id)
        {
            if (DateTime.Now.Hour > 22 && DateTime.Now.Hour < 9)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }

            var deletedCategory = _categoryDal.Get(p => p.CategoryId == id);
            _categoryDal.Delete(deletedCategory);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
