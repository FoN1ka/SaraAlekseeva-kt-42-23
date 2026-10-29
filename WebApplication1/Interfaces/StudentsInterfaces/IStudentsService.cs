using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.StudentsInterfaces
{
    public interface IStudentService {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        Task AddStudentAsync(string firstName, string lastName, int groupId, CancellationToken ct);
        Task UpdateStudentAsync(int studentId, string newFirstName, string newLastName, int newGroupId, CancellationToken ct);
        Task DeleteStudentAsync(int studentId, CancellationToken ct);
    }
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext) { _dbContext = dbContext; }

        public async Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken=default)
        {
            // базовый запрос к таблице студентов
            var query = _dbContext.Set<Student>().AsQueryable();

            // Фильтрация по группе
            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                query = query.Where(w => w.Group.Name == filter.GroupName);
            }

            // Фильтрация по ФИО (ищем совпадение в Имени или Фамилии)
            if (!string.IsNullOrWhiteSpace(filter.StudentFullName))
            {
                query = query.Where(w => w.FirstName.Contains(filter.StudentFullName) || 
                                         w.LastName.Contains(filter.StudentFullName));
            }

            // Фильтрация по статусу удаления
            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(w => w.IsDeleted == filter.IsDeleted.Value);
            }


            var students = await query
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId
                })
                .ToArrayAsync(cancellationToken);

            return students;
        }

        public async Task AddStudentAsync(string firstName, string lastName, int groupId, CancellationToken ct)
        {
            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                GroupId = groupId,
            };

            await _dbContext.Set<Student>().AddAsync(student, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task UpdateStudentAsync(int studentId, string firstName, string lastName, int groupId, CancellationToken ct)
        {
            var student = await _dbContext.Set<Student>().FindAsync(new object[] { studentId }, ct);
            if (student != null)
            {
                student.FirstName = firstName;
                student.LastName = lastName;
                student.GroupId = groupId;

                await _dbContext.SaveChangesAsync(ct);
            }
        }

        public async Task DeleteStudentAsync(int studentId, CancellationToken ct)
        {
            var student = await _dbContext.Set<Student>().FindAsync(new object[] { studentId }, ct);
            if (student != null)
            {
                student.IsDeleted = true;
                await _dbContext.SaveChangesAsync(ct);
            }
        }
    }
}
