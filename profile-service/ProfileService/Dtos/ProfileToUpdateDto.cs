using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Dtos
{
    public class ProfileToUpdateDto
    {
        [Required(AllowEmptyStrings = true)]
        public string ProfilePictureUrl { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Bio { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Location { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Website { get; set; }
    }
}
