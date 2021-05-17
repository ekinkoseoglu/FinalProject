using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface ICategoryService
   {
       IDataResult<List<Category>> GetAll();
       IDataResult<Category> GetById(int Categoryid);
       IResult Add(Category category);
       IResult Update(Category category);
       IResult Delete(int id);
    }
}
