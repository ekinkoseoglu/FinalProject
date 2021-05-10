using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
   public class CategoryManager:ICategoryService
   {
       private ICategoryDal _categoryDal;

       public CategoryManager(ICategoryDal categoryDal)
       {
           _categoryDal = categoryDal;
       }

       public List<Category> GetAll()
        {
            // İş kodları
            return _categoryDal.GetAll();
        }

        public Category GetById(int cId)
        {
            return _categoryDal.Get(c=>c.CategoryId== cId); // CATEGORY SERVŞCE YAPTIK İÇİNİ İMZALARLA YAZDIK SUAN MANAGER'İNİ YAZIYORUZ
        }
    }
}
