using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.StudentsInterfaces
{
    public interface IDisciplineService
    {
        Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken ct);
        Task AddDisciplineAsync(Discipline discipline, CancellationToken ct);
        Task UpdateDisciplineAsync(Discipline discipline, CancellationToken ct);
        Task DeleteDisciplineAsync(int disciplineId, CancellationToken ct);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;
        public DisciplineService(StudentDbContext dbContext) => _dbContext = dbContext;

        public async Task<Discipline[]> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Discipline>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Direction))
                query = query.Where(d => d.Direction == filter.Direction);

            if (filter.IsDeleted.HasValue)
                query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);

            var disciplines = await query
                .Select(s => new Discipline
                {
                    DisciplineId = s.DisciplineId,
                    Name = s.Name,
                    Direction = s.Direction,
                    IsDeleted = s.IsDeleted,
                    Grades = null
                })
                .ToArrayAsync(cancellationToken);

            return disciplines;
        }

        public async Task AddDisciplineAsync(Discipline discipline, CancellationToken ct)
        {
            await _dbContext.Set<Discipline>().AddAsync(discipline, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateDisciplineAsync(Discipline discipline, CancellationToken ct)
        {
            _dbContext.Set<Discipline>().Update(discipline);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteDisciplineAsync(int disciplineId, CancellationToken ct)
        {
            var discipline = await _dbContext.Set<Discipline>().FindAsync(new object[] { disciplineId }, ct);
            if (discipline != null)
            {
                discipline.IsDeleted = true;
                await _dbContext.SaveChangesAsync(ct);
            }
        }
    }
}
