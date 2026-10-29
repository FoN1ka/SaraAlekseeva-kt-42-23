using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.StudentsInterfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupBySpecialnostAsync(GroupFilter filter, CancellationToken cancellationToken);
        Task AddGroupAsync(string name, int course, int specialnostId, CancellationToken ct);
        Task UpdateGroupAsync(int groupId, string newName, int newCourse, int newSpecialnostId, CancellationToken ct);
        Task DeleteGroupAsync(int groupId, CancellationToken ct); // Каскадное удаление
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

        public async Task AddGroupAsync(string name, int course, int specialnostId, CancellationToken ct)
        {
            var group = new Group
            {
                Name = name,
                Course = course,
                SpecialnostId = specialnostId,
            };

            await _dbContext.Set<Group>().AddAsync(group, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateGroupAsync(int groupId, string newName, int newCourse, int newSpecialnostId, CancellationToken ct)
        {
            var group = await _dbContext.Set<Group>().FindAsync(new object[] { groupId }, ct);
            if (group != null)
            {
                group.Name = newName;
                group.Course = newCourse;
                group.SpecialnostId = newSpecialnostId;

                await _dbContext.SaveChangesAsync(ct);
            }
        }

        public async Task DeleteGroupAsync(int groupId, CancellationToken ct)
        {
            var group = await _dbContext.Set<Group>().FindAsync(new object[] { groupId }, ct);
            if (group == null) return;

            group.IsDeleted = true; // Мягкое удаление группы

            // Каскадное удаление студентов
            var students = await _dbContext.Set<Student>().Where(s => s.GroupId == groupId).ToListAsync(ct);
            foreach (var student in students)
            {
                student.IsDeleted = true;
            }

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
