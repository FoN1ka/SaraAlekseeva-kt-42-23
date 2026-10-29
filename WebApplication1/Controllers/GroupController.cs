using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Interfaces.StudentsInterfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;

        public GroupController(ILogger<GroupController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpPost(Name = "GetGroupBySpecialnost")]
        public async Task<IActionResult> GetStudentsByGroupAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupBySpecialnostAsync(filter, cancellationToken);

            return Ok(groups);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroupAsync([FromBody] AddGroupDto dto, CancellationToken ct)
        {
            await _groupService.AddGroupAsync (dto.Name, dto.Course, dto.SpecialnostId, ct);
            return Ok("Группа добавлена");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] UpdateGroupDto dto, CancellationToken ct)
        {
            await _groupService.UpdateGroupAsync(dto.GroupId, dto.NewName, dto.NewCourse, dto.NewSpecialnostId, ct);
            return Ok("Группа обновлена");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupAsync(int id, CancellationToken ct)
        {
            await _groupService.DeleteGroupAsync(id, ct);
            return Ok("Группа удалена (soft delete)");
        }
    }
}
