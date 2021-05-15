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

        public Category GetById(int cId) // select * from Categories where CategoryId = ???
        {
            return _categoryDal.Get(c=>c.CategoryId== cId); // CATEGORY SERVŞCE YAPTIK İÇİNİ İMZALARLA YAZDIK SUAN MANAGER'İNİ YAZIYORUZ
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }

        public void Delete(int id)
        {
            var deletedCategory = _categoryDal.Get(p => p.CategoryId == id);
            _categoryDal.Delete(deletedCategory);
        }
   }
}
