using System;
using ProfileService.Dtos;

namespace ProfileService.Data
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProfileToCreateDto, Profile>();
            CreateMap<ProfileToUpdateDto, Profile>();

        }
    }
}
