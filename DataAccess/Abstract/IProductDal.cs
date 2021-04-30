using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
   public interface IProductDal:IEntityRepository<Product> // Sen bir IEntityRepository'sin ve çalışma tipin "Product"'dır dedik
   {
      
   }
}
