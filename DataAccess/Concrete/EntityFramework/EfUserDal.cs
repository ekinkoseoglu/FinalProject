 using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new NorthwindContext()) // UserId ile UserOperationClaim'deki UserId uyuşan columnun sahip olduğu claimleri OperationClaim tablosundan getir
            {
                var result = from operationClaim in context.OperationClaims // OperationClaimler'le UserOperationClaimlerejoin atıyor
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id // Onlar içerisinde Id'si benim gönderdiğim User'a eşit olan Id'yi buluyor
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name }; // perationClaim olarak bunları return ediyor.
                return result.ToList();

            }
        }
    }
}
