using System;
using System.Threading.Tasks;
using ProfileService.Data;
using ProfileService.Interfaces;

namespace ProfileService.Messaging
{
    public class AccountCreatedMessageHandler : MessageHandler<Profile>
    {
        private readonly IProfileRepository _profileRepository;
        public AccountCreatedMessageHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public override async Task HandleMessageAsync(string messageType, Profile message)
        {
            await _profileRepository.CreateProfileAsync(message);
        }
    }
}
