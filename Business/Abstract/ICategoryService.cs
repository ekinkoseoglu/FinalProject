using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface ICategoryService
   {
       List<Category> GetAll();
       Category GetById(int Categoryid);
       void Add(Category category);
       void Update(Category category);
       void Delete(int id);
    }
}
