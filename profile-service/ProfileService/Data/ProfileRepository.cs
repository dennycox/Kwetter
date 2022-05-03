using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Interfaces;

namespace ProfileService.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Profile> GetProfileByIdAsync(Guid id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Profile> GetProfileByUserIdAsync(string userId)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IReadOnlyList<Profile>> GetProfilesAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return profile;
        }
        public async Task<Profile> UpdateProfileAsync(Guid profileId, Profile updatedProfile)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile == null) return null;

            profile.ProfilePictureUrl = updatedProfile.ProfilePictureUrl;
            profile.Name = updatedProfile.Name;
            profile.Website = updatedProfile.Website;
            profile.Location = updatedProfile.Location;
            profile.Bio = updatedProfile.Bio;

            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();

            return profile;
        }

        public async Task<Profile> DeleteProfileAsync(Guid profileId)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile == null) return null;

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return profile;
        }
    }
}
