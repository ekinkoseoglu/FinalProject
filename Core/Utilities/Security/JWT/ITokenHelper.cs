using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper // Bugün JWT ile yaparım yarın başka bir teknik ile yaparım. O yüzden Interface yapıyorum
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); // Kullanıcı bilgisi ve Kullanıcının rollerini verip onların da şuan yarattığımız token'a eklenmesini istiyorum.

    }
}
