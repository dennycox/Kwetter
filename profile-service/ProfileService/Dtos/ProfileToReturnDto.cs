using System;
namespace ProfileService.Dtos
{
    public class ProfileToReturnDto
    {
        public Guid Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
    }
}
