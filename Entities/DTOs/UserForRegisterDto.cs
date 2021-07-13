﻿using Core.Entities;

namespace Entities.DTOs
{
    public class UserForRegisterDto : IDto // Sistemimize kayıt olmak isteyen bir kişinin entity'si 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}