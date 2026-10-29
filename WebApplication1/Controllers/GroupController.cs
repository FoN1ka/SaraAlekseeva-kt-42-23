using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Interfaces.StudentsInterfaces;

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

        [HttpPost(Name = "GetStudentByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupBySpecialnostAsync(filter, cancellationToken);

            return Ok(groups);
        }
    }
}
