using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products // İki tablonun da kısaltılmıs harfini yazmalıyız  çünkü altta göründüğü gibi kullanmak zorunda kalacağız
                             join c in context.Categories
                                 on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto // Sonucu şu konulara uydurarak ver (Hangi özellikleri hangi tablodan alacağımızı burada belirliyoruz)
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 UnitsInStock = p.UnitsInStock,
                                 CategoryName = c.CategoryName
                             };
                return result.ToList();
            }
        }
    }
}
