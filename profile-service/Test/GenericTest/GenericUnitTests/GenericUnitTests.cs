using AutoMapper;
using ProfileService.Data;
using ProfileService.Interfaces;
using System;
using System.Linq;
using Test.InMemoryData;
using Xunit;

namespace Test.GenericTest.GenericUnitTests
{
    public class GenericUnitTests
    {
        private IMapper _mapper;

        public GenericUnitTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
        }

        [Theory]
        [InlineData("b6f8c4bc-1b8d-4661-b129-a4e51f34de87", "b6f8c4bc-1b8d-4661-b129-a4e51f34de87")]
        public async void Test_GetById(string availableProfileId, string expectedProfileId)
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IGenericRepository<ProfileService.Data.Profile> genericRepository = new GenericRepository<ProfileService.Data.Profile>(context);

            var profile = await genericRepository.GetByIdAsync(new Guid(availableProfileId));

            Assert.Equal(expectedProfileId, profile.Id.ToString());
        }

        [Fact]
        public async void Test_ListAll()
        {
            var context = await InMemoryUnitTestDataGenerator.Initialize();
            IGenericRepository<ProfileService.Data.Profile> genericRepository = new GenericRepository<ProfileService.Data.Profile>(context);

            var profiles = await genericRepository.ListAllAsync();

            Assert.True(profiles.Count > 0);
        }
    }
}
