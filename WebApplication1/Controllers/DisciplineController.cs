using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Interfaces.StudentsInterfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplineController : ControllerBase
    {
        private readonly ILogger<DisciplineController> _logger;
        private readonly IDisciplineService _disciplineService;

        public DisciplineController(ILogger<DisciplineController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            _disciplineService = disciplineService;
        }

        [HttpPost(Name = "GetDisciplinesAsync")]
        public async Task<IActionResult> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(filter, cancellationToken);

            return Ok(disciplines);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddDiscipline([FromBody] Discipline discipline, CancellationToken ct)
        {
            await _disciplineService.AddDisciplineAsync(discipline, ct);
            return Ok("Дисциплина добавлена");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscipline([FromBody] Discipline discipline, CancellationToken ct)
        {
            await _disciplineService.UpdateDisciplineAsync(discipline, ct);
            return Ok("Дисциплина обновлена");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id, CancellationToken ct)
        {
            await _disciplineService.DeleteDisciplineAsync(id, ct);
            return Ok("Дисциплина удалена (soft delete)");
        }
    }
}
