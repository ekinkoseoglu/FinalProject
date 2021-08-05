using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto); // Kullanıcıyı kontrol edelim
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var token = _authService.CreateAccessToken(userToLogin.Data); // İşlem başarılıysa o kullanıcıya AccessToken veriyoruz
            if (token.Success)
            {
                return Ok(token.Data);
            }

            return BadRequest(token.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email); // User zaten var mı kontrol ediyoruz.
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto); // Register ettik kullanıcıyı
            var result = _authService.CreateAccessToken(registerResult.Data); // şimdide "RegisterResult" bir user döndürüyor ve ben o User için bir AccesToken yaratıyorum.
            if (result.Success)
            {
                return Ok(registerResult.Message);
            }

            return BadRequest(result.Message);

        }


    }
}
