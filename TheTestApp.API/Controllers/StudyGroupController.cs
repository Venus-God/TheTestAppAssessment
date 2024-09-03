using Microsoft.AspNetCore.Mvc;
using TheTestApp.API.DataLayer;
using TheTestApp.API.Models;
using TheTestApp.API.Enums;

namespace TheTestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyGroupController : ControllerBase
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudyGroup([FromBody] StudyGroup studyGroup)
        {
            if (studyGroup == null || string.IsNullOrWhiteSpace(studyGroup.Name) || studyGroup.Name.Length < 5 || studyGroup.Name.Length > 30)
            {
                return BadRequest("Invalid study group name.");
            }
            //I could add functionality to check for duplicates

            await _studyGroupRepository.CreateStudyGroup(studyGroup);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            if (!studyGroups.Any())
            {
                return NotFound("No study groups found");
            }
            return Ok(studyGroups);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchStudyGroups([FromQuery] string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return Ok(studyGroups);
        }

        [HttpPost("{studyGroupId}/join")]
        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, [FromBody] int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return Ok();
        }

        [HttpPost("{studyGroupId}/leave")]
        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, [FromBody] int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return Ok();
        }
    }
}
