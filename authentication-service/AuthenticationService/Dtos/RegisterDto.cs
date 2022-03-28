using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = true)]
        public string Bio { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Location { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Website { get; set; }
    }
}
