using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>  //Sen bir IEntityRepository'sin ve çalışma tipin "Category"'dır dedik
    {
    }
}
