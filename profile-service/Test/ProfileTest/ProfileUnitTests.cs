using AutoMapper;
using ProfileService.Data;
using ProfileService.Dtos;
using ProfileService.Interfaces;
using System;
using System.Linq;
using Test.InMemoryData;
using Test.Messaging;
using Xunit;

namespace Test.ProfileTest.ProfileUnitTests
{
    public class ProfileUnitTests
    {
        private IMapper _mapper;

        public ProfileUnitTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });

            _mapper = mockMapper.CreateMapper();
        }

        [Theory]
        [InlineData("b6f8c4bc-1b8d-4661-b129-a4e51f34de87", "b6f8c4bc-1b8d-4661-b129-a4e51f34de87")]
        public async void Test_GetProfileById(string availableProfileId, string expectedProfileId)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profile = await profileRepository.GetProfileByIdAsync(new Guid(availableProfileId));

            Assert.Equal(expectedProfileId, profile.Id.ToString());
        }

        [Theory]
        [InlineData("b6f8c4bc-1b8d-4661-b129-a4e51f34de00")]
        public async void Test_GetProfileByIdNotExisting(string nonExistingProfileId)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profile = await profileRepository.GetProfileByIdAsync(new Guid(nonExistingProfileId));

            Assert.Null(profile);
        }

        [Theory]
        [InlineData("42048812-decb-4e22-89d7-3dcb78a2645f", "b6f8c4bc-1b8d-4661-b129-a4e51f34de87")]
        public async void Test_GetProfileByUserId(string availableUserId, string expectedProfileId)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profile = await profileRepository.GetProfileByUserIdAsync(availableUserId);

            Assert.Equal(expectedProfileId, profile.Id.ToString());
        }

        [Theory]
        [InlineData("b6f8c4bc-1b8d-4661-b129-a4e51f34deff")]
        public async void Test_GetProfileByUserIdNotExisting(string nonExistingUserId)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profile = await profileRepository.GetProfileByUserIdAsync(nonExistingUserId);

            Assert.Null(profile);
        }

        [Fact]
        public async void Test_GetProfiles()
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profiles = await profileRepository.GetProfilesAsync();

            Assert.True(profiles.Count > 0);
        }

        [Theory]
        [InlineData("b42a7161-00d0-4994-86fb-120b840aa0a4", "", "test", "Hello", "Netherlands", "www.test.com")]
        public async void Test_CreateProfile(string userId, string profilePictureUrl, string name, string bio, string location, string website)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profileToCreate = new ProfileToCreateDto
            {
                UserId = userId,
                ProfilePictureUrl = profilePictureUrl,
                Name = name,
                Bio = bio,
                Location = location,
                Website = website
            };

            var profile = _mapper.Map<ProfileToCreateDto, ProfileService.Data.Profile>(profileToCreate);

            var createdProfile = await profileRepository.CreateProfileAsync(profile);

            Assert.Equal(userId, createdProfile.UserId);
            Assert.Equal(profilePictureUrl, createdProfile.ProfilePictureUrl);
            Assert.Equal(name, createdProfile.Name);
            Assert.Equal(bio, createdProfile.Bio);
            Assert.Equal(location, createdProfile.Location);
            Assert.Equal(website, createdProfile.Website);
        }

        [Theory]
        [InlineData("b6f8c4bc-1b8d-4661-b129-a4e51f34de87", "", "test2", "Hey", "Netherlands", "www.test2.com")]
        public async void Test_UpdateProfile(string profileId, string profilePictureUrl, string profileName, string bio, string location, string website)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profileToUpdate = new ProfileToUpdateDto
            {
                ProfilePictureUrl = profilePictureUrl,
                Name = profileName,
                Bio = bio,
                Location = location,
                Website = website
            };

            var profile = _mapper.Map<ProfileToUpdateDto, ProfileService.Data.Profile>(profileToUpdate);

            var updatedProfile = await profileRepository.UpdateProfileAsync(new Guid(profileId), profile);

            Assert.Equal(profileId, updatedProfile.Id.ToString());
            Assert.Equal(profilePictureUrl, updatedProfile.ProfilePictureUrl);
            Assert.Equal(profileName, updatedProfile.Name);
            Assert.Equal(bio, updatedProfile.Bio);
            Assert.Equal(location, updatedProfile.Location);
            Assert.Equal(website, updatedProfile.Website);
        }

        [Theory]
        [InlineData("b6f8c4bc-1b8d-4661-b129-a4e51f34de87")]
        public async void Test_DeleteProfile(string availableProfileId)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IProfileRepository profileRepository = new ProfileRepository(context);

            var profileDeleted = await profileRepository.DeleteProfileAsync(new Guid(availableProfileId));

            Assert.NotNull(profileDeleted);
        }
    }
}
