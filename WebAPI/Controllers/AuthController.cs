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

            var result = _authService.CreateAccessToken(userToLogin.Data); // İşlem başarılıysa o kullanıcıya AccessToken veriyoruz
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email); // User zaten var mı kontrol ediyoruz.
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto); // Register
            var result = _authService.CreateAccessToken(registerResult.Data);// "RegisterResult" bir user döndürüyor ve o User için bir AccesToken yaratıyoruz.
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);

        }


    }
}
