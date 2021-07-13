using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService // Bu Servis ile sisteme kayıt olacağım veya giriş yapacağım
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email); // Kullanıcı var mı?
        IDataResult<AccessToken> CreateAccessToken(User user); // Frontend de kayıt olan kişinin kayıt olduktan sonra kullanıcıya bir token vermemiz için yaratacağımız token

    }
}