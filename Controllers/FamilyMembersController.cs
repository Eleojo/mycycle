using Core.FamilyMemberService;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MyCycleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyMembersController : ControllerBase
    {
        private readonly IFamilyMemberService _familyMemberService;
        private readonly ILogger<FamilyMembersController> _logger;

        public FamilyMembersController(IFamilyMemberService familyMemberService, ILogger<FamilyMembersController> logger)
        {
            _familyMemberService = familyMemberService;
            _logger = logger;
        }

        [HttpPost("add-new-family-member")]
        public async Task<IActionResult> AddFamilyMember(FamilyMemberDto familyMember)
        {
            try
            {
                _logger.LogInformation("Adding new family member");
                await _familyMemberService.AddNewFamilyMemberAsync(familyMember);
                return Ok("Congratulations!! You have gained a new family member");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Family member not added something went wrong");
                return BadRequest("Family member not added something went wrong");
            }
        }

        [HttpDelete("delete")]

        public async Task<IActionResult> RemoveFamilyMember(Guid Id)
        {
            var familyMemberExists = await _familyMemberService.RemoveFamilyMemberAsync(Id);
            if (!familyMemberExists)
            {
                _logger.LogWarning($"FamilyMember with ID {Id} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Family Member with ID {Id} successfully hard deleted.");
            return NoContent();
        }

        [HttpGet("retrieve-family-member-details")]
        public async Task<IActionResult> GetFamilyMember(Guid Id)
        {
            var familyMember = await _familyMemberService.GetFamilyMemberDetailsAsync(Id);
            if (familyMember == null)
            {
                _logger.LogInformation($"Sorry no family member with Id: {Id}");
                return NotFound();
            }
            _logger.LogInformation($"Successfully fetched details for family member: {familyMember.FirstName}");
            return Ok(familyMember);

        }
    }
}
