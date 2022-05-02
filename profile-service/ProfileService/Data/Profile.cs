using System;
using System.Collections.Generic;

namespace ProfileService.Data
{
    public class Profile : BaseEntity
    {
        public Profile()
        {

        }

        public Profile(string userId, string profilePictureUrl, string profileName, string bio, string location, string website)
        {
            UserId = userId;
            ProfilePictureUrl = profilePictureUrl;
            Name = Name;
            Bio = bio;
            Location = location;
            Website = website;
        }

        public Profile(Guid id, string userId, string profilePictureUrl, string profileName, string bio, string location, string website)
        {
            Id = id;
            UserId = userId;
            ProfilePictureUrl = profilePictureUrl;
            Name = Name;
            Bio = bio;
            Location = location;
            Website = website;
        }
        public string UserId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
    }
}
