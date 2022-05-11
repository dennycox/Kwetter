using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Dtos;
using ProfileService.Interfaces;

namespace ProfileService.Data
{
    public class ProfileManager : IProfileManager
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IGenericRepository<Profile> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        private readonly ILogger _logger;

        public ProfileManager(IProfileRepository profileRepository, IGenericRepository<Profile> genericRepository, IMapper mapper, IPublisher publisher, ILogger<ProfileManager> logger)
        {
            _profileRepository = profileRepository;
            _genericRepository = genericRepository;
            _mapper = mapper;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<ProfileToReturnDto> CreateProfile(ProfileToCreateDto profileToCreate)
        {
            try
            {
                var profile = _mapper.Map<ProfileToCreateDto, Profile>(profileToCreate);
                var createdProfile = await _profileRepository.CreateProfileAsync(profile);
                return _mapper.Map<Profile, ProfileToReturnDto>(createdProfile);
            }
            catch(Exception)
            {
                _logger.Log(LogLevel.Error, $"Failed to create new profile. At: { DateTime.Now }");
                return null;
            }
        }

        public async Task<bool> DeleteProfile(Guid profileId)
        {
            try
            {
                Profile deletedProfile =  await _profileRepository.DeleteProfileAsync(profileId);                
                if (deletedProfile == null) return false;
                _publisher.Publish(JsonConvert.SerializeObject(deletedProfile), "profile.delete", null);
                return true;
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, $"Failed to delete profile with id: { profileId }. At: { DateTime.Now }");
                return false;
            }
        }

        public async Task<ProfileToReturnDto> GetProfileById(Guid id)
        {
            try
            {
                var profile = await _profileRepository.GetProfileByIdAsync(id);
                return _mapper.Map<Profile, ProfileToReturnDto>(profile);
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, $"Failed to retrieve profile by id: { id }. At: { DateTime.Now }");
                return null;
            }
        }

        public async Task<ProfileToReturnDto> GetProfileByUserId(string userId)
        {
            try
            {
                var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
                var dto = new ProfileToReturnDto()
                {
                    Name = profile.Name,
                    ProfilePictureUrl = profile.ProfilePictureUrl,
                    Bio = profile.Bio,
                    Id = profile.Id,
                    Location = profile.Location,
                    Website = profile.Website
                };

                return dto;
            }
            catch (Exception)
            {
                _logger.Log(LogLevel.Error, $"Failed to retrieve profile by user id: { userId }. At: { DateTime.Now }");
                return null;
            }
        }

        public async Task<ProfileToReturnDto> UpdateProfile(Guid profileId, ProfileToUpdateDto profileToUpdate)
        {
            try
            {
                var profile = _mapper.Map<ProfileToUpdateDto, Profile>(profileToUpdate);
                var updatedProfile = await _profileRepository.UpdateProfileAsync(profileId, profile);
                if (updatedProfile == null) return null;                
                _publisher.Publish(JsonConvert.SerializeObject(updatedProfile), "profile.update", null);
                return _mapper.Map<Profile, ProfileToReturnDto>(updatedProfile);
            }
            catch(Exception)
            {
                _logger.Log(LogLevel.Error, $"Failed to update profile with id: { profileId }. At: { DateTime.Now }");
                return null;
            }
        }
    }
}
