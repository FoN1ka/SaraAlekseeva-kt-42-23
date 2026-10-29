using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Interfaces.StudentsInterfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger= logger;
            _studentService= studentService;
        }

        [HttpPost(Name = "GetStudentByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok (students);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentDto dto, CancellationToken ct)
        {
            await _studentService.AddStudentAsync(dto.FirstName, dto.LastName, dto.GroupId, ct);
            return Ok("Студент добавлен");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentDto dto, CancellationToken ct)
        {
            await _studentService.UpdateStudentAsync(dto.StudentId, dto.FirstName, dto.LastName, dto.GroupId, ct);
            return Ok("Студент обновлен");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAsync(int id, CancellationToken ct)
        {
            await _studentService.DeleteStudentAsync(id, ct);
            return Ok("Студент удален (soft delete)");
        }
    }
}
