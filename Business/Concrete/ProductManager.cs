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
    }
}
