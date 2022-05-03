using System;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Data;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Messaging
{
    public class ProfileDeletedMessageHandler : MessageHandler<Profile>
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfileDeletedMessageHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task HandleMessageAsync(string messageType, Profile message)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == message.UserId);
            await _userManager.DeleteAsync(user);
        }
    }
}
