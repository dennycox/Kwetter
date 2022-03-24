using System;

namespace AuthenticationService.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
    }
}
