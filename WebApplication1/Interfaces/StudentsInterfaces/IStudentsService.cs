using Microsoft.EntityFrameworkCore;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Models;

namespace WebApplication1.Interfaces.StudentsInterfaces
{
    public interface IStudentService {
        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext) { _dbContext = dbContext; }

        public Task<Student[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken=default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.Name == filter.GroupName).ToArrayAsync(cancellationToken);
            return students;
        }
    }
}
