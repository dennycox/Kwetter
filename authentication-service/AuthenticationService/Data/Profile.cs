using System;
namespace AuthenticationService.Data
{
    public class Profile
    {
        public Profile()
        {
        }

        public Profile(string userId, string profilePictureUrl, string name, string bio, string location, string website)
        {
            UserId = userId;
            ProfilePictureUrl = profilePictureUrl;
            Name = name;
            Bio = bio;
            Location = location;
            Website = website;
        }

        public Profile(Guid id, string userId, string profilePictureUrl, string name, string bio, string location, string website)
        {
            Id = id;
            UserId = userId;
            ProfilePictureUrl = profilePictureUrl;
            Name = name;
            Bio = bio;
            Location = location;
            Website = website;
        }

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
    }
}
