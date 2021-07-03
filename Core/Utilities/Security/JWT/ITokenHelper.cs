using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.Security.JWT
{
    interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<UserOperationClaim> OperationClaims);
    }
}
