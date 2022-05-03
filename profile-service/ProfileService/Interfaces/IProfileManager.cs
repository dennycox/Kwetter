using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Data;
using ProfileService.Dtos;

namespace ProfileService.Interfaces
{
    public interface IProfileManager
    {
        Task<ProfileToReturnDto> GetProfileById(Guid id);
        Task<ProfileToReturnDto> GetProfileByUserId(string userId);
        Task<ProfileToReturnDto> CreateProfile(ProfileToCreateDto profileToCreate);
        Task<ProfileToReturnDto> UpdateProfile(Guid profileId, ProfileToUpdateDto profileToUpdate);
        Task<bool> DeleteProfile(Guid profileId);
    }
}
