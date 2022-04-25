using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Data;

namespace ProfileService.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileByIdAsync(Guid id);
        Task<Profile> GetProfileByUserIdAsync(string userId);
        Task<IReadOnlyList<Profile>> GetProfilesAsync();
        Task<Profile> CreateProfileAsync(Profile profile);
        Task<Profile> UpdateProfileAsync(Guid profileId, Profile updatedProfile);
        Task<Profile> DeleteProfileAsync(Guid profileId);
    }
}
