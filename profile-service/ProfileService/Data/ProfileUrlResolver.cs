using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ProfileService.Dtos;

namespace ProfileService.Data
{
    public class ProfileUrlResolver : IValueResolver<Profile, ProfileToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProfileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Profile source, ProfileToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProfilePictureUrl))
            {
                return _config["ApiUrl"] + source.ProfilePictureUrl;
            }

            return null;
        }
    }
}
