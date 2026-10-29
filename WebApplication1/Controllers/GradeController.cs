using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Interfaces.StudentsInterfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradeController: ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly IGradeService _gradeService;

        public GradeController(ILogger<GradeController> logger, IGradeService gradeService)
        {
            _logger = logger;
            _gradeService = gradeService;
        }

        [HttpPost("GetAverageGradeForGroupAsync")]
        public async Task<IActionResult> GetAverageGradeForGroupAsync(int groupId, int disciplineId, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.GetAverageGradeForGroupAsync(groupId, disciplineId, cancellationToken);

            return Ok(grades);
        }
        [HttpPost("GetStudentGradesAsync")]
        public async Task<IActionResult> GetStudentGradesAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.GetStudentGradesAsync(studentId, cancellationToken);

            return Ok(grades);
        }
        [HttpPost("GetAverageGradeForYearAsync")]
        public async Task<IActionResult> GetAverageGradeForYearAsync(int course, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.GetAverageGradeForYearAsync(course, cancellationToken);

            return Ok(grades);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddGradeAsync(int gradeValue, int studentId, int disciplineId, CancellationToken ct)
        {
            await _gradeService.AddGradeAsync (gradeValue, studentId, disciplineId, ct);
            return Ok("Оценка добавлена");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGradeAsync(int gradeId, int newValue, CancellationToken ct)
        {
            await _gradeService.UpdateGradeAsync(gradeId, newValue, ct);
            return Ok("Оценка обновлена");
        }
    }
}
