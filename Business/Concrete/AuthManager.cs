using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService; // Kullanıcıyı kontrol etmemiz gerekiyor
        private ITokenHelper _tokenHelper;// Kullanıcı login olduğunda ona bir token vermemiz gerek o yüzden bu interfaceyi de kullanmamız gerek


        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt; // Biz bu değerleri "HashingHelper" classına boş gönderiyoruz.
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt); // "HashingHelper" class'ında oluşan değerler üstteki değerlere doldurulup değişiyor çünkü "out" keyword ile gönderiyoruz
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email); // Öncelikle böyle bir kullanıcı var mı ?
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt)) // Kullanıcının bize gönderdiği şifreyi hashleyip databasemizdeki hash ile aynı olup olmadığını kontrol ediyoruz. 

            // Kullanıcının  girdiği password eğer bizim User değeri tutan userToCheck Nesnesindeki bizim databasemizdeki User'in "PasswordSalt" algoritmasıyla hashlanmış "PasswordHash" ile Verify edilemiyorsa Şifre hatalı 
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            var user = _userService.GetByMail(email);
            if (user != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) // JwtHelper'daki "CreateToken" Methodu ile UserManager'daki "GetClaims" claimlerini çekerek Token yaratıyor. 
        {
            var claims = _userService.GetClaims(user); // Kullanıcının Claimlerini
            var accessToken = _tokenHelper.CreateToken(user, claims); // User'i ve Claimlerini alıp ona uygun token'i yaratıyoruz
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated); // AccessToken'i döndürsün
        }



    }
}
