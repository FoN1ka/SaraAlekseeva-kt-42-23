using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.StudentsInterfaces
{
    public interface IGradeService
    {
        Task<Grade[]> GetStudentGradesAsync(int studentId, CancellationToken ct);
        Task<double> GetAverageGradeForGroupAsync(int groupId, int disciplineId, CancellationToken ct);
        Task<double> GetAverageGradeForYearAsync(int course, CancellationToken ct);

        //Добавление и изменение оценок
        Task AddGradeAsync(Grade grade, CancellationToken ct);
        Task UpdateGradeAsync(Grade grade, CancellationToken ct);
    }

    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;
        public GradeService(StudentDbContext dbContext) => _dbContext = dbContext;

        // Средний балл по конкретному предмету в конкретной группе
        public async Task<double> GetAverageGradeForGroupAsync(int groupId, int disciplineId, CancellationToken ct)
        {
            var grades = await _dbContext.Set<Grade>().Where(g => g.Student.GroupId == groupId && g.DisciplineId == disciplineId)
                .Select(g => g.Value)
                .ToListAsync(ct);

            return grades.Any() ? grades.Average() : 0;
        }

        // Все оценки конкретного студента
        public async Task<Grade[]> GetStudentGradesAsync(int studentId, CancellationToken ct)
        {
            return await _dbContext.Set<Grade>()
                .Where(g => g.StudentId == studentId)
                .Select(g => new Grade
                {
                    GradeId = g.GradeId,
                    Value = g.Value,
                    DisciplineId = g.DisciplineId,
                    StudentId = g.StudentId,
                    Discipline = null,
                    Student = null
                })
                .ToArrayAsync(ct);
        }

        // Средний балл по всем предметам для конкретного курса (года)
        public async Task<double> GetAverageGradeForYearAsync(int course, CancellationToken ct)
        {
            var grades = await _dbContext.Set<Grade>()
                .Where(g => g.Student.Group.Course == course)
                .Select(g => g.Value)
                .ToListAsync(ct);

            return grades.Any() ? grades.Average() : 0;
        }

        public async Task AddGradeAsync(Grade grade, CancellationToken ct)
        {
            await _dbContext.Set<Grade>().AddAsync(grade, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateGradeAsync(Grade grade, CancellationToken ct)
        {
            _dbContext.Set<Grade>().Update(grade);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
