using Core.Entities;

namespace Entities.DTOs
{
    public class UserForLoginDto : IDto // Login olacak Kişinin Email ve parolasını isteriz, onun için
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}