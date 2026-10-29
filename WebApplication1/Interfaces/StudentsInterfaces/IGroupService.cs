using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.StudentsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupBySpecialnostAsync(GroupFilter filter, CancellationToken cancellationToken);
    }

    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;

        public GroupService (StudentDbContext dbContext) { _dbContext = dbContext; }

        public async Task<Group[]> GetGroupBySpecialnostAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            // базовый запрос к таблице студентов
            var query = _dbContext.Set<Group>().AsQueryable();

            // Фильтрация по специальности
            if (!string.IsNullOrWhiteSpace(filter.SpecialnostName))
            {
                query = query.Where(w => w.Specialnost.Title == filter.SpecialnostName);
            }

            // Фильтрация по году (по курсу)
            if (filter.Course.HasValue)
            {
                query = query.Where(w => w.Course == filter.Course.Value);
            }

            // Фильтрация по статусу удаления
            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(w => w.IsDeleted == filter.IsDeleted.Value);
            }


            var groups = await query
                .Select(s => new Group
                {
                    GroupId = s.GroupId,
                    Course = s.Course,
                    Name = s.Name,
                    SpecialnostId = s.SpecialnostId,
                    IsDeleted = s.IsDeleted,
                    Specialnost = null
                })
                .ToArrayAsync(cancellationToken);

            return groups;
        }
    }
}
