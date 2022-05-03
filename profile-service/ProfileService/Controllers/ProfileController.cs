using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Data;
using ProfileService.Dtos;
using ProfileService.Interfaces;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileManager _profileManager;
        public ProfileController(IProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfileToReturnDto>> GetProfile(Guid id)
        {
            var profile = await _profileManager.GetProfileById(id);
            if (profile == null) return NotFound(new ApiResponse(404));
            return Ok(profile);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ProfileToReturnDto>> GetProfileByUserId(string userId)
        {
            var profile = await _profileManager.GetProfileByUserId(userId);
            if (profile == null) return NotFound(new ApiResponse(404));
            return Ok(profile);
        }

        [HttpPost]
        public async Task<ActionResult<ProfileToReturnDto>> CreateProfile(ProfileToCreateDto profileToCreate)
        {
            var createdProfile = await _profileManager.CreateProfile(profileToCreate);
            if (createdProfile == null) return BadRequest(new ApiResponse(400));
            return Ok(createdProfile);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProfileToReturnDto>> UpdateProfile(Guid id, ProfileToUpdateDto profileToUpdate)
        {
            var updatedProfile = await _profileManager.UpdateProfile(id, profileToUpdate);
            if (updatedProfile == null) return BadRequest(new ApiResponse(400));
            return Ok(updatedProfile);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProfile(Guid id)
        {
            bool succeeded = await _profileManager.DeleteProfile(id);
            if (!succeeded) return BadRequest(new ApiResponse(400));
            return Ok();
        }
    }
}
