using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.DataBase;
using WebApplication1.Filters.StudentsFilters;
using WebApplication1.Interfaces.StudentsInterfaces;
using WebApplication1.Models;

namespace SaraAlekseevaKT_42_23.Tests
{
    public class StudentIntegrationTests
    {
        public readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public StudentIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "student_db")
                .Options;
        }

        [Fact]
        public async Task GetStudentsByGroupAsync_KT4223_TwoObjects()
        {
            // Arrange
            using var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);

            var groups = new List<Group>
            {
                new Group { Name = "КТ-31-20" },
                new Group { Name = "КТ-41-20" }
            };
            await ctx.Set<Group>().AddRangeAsync(groups);
            await ctx.SaveChangesAsync();

            var students = new List<Student>
            {
                new Student { FirstName = "qwerty", LastName = "asdf", GroupId = groups[0].GroupId },
                new Student { FirstName = "qwerty2", LastName = "asdf2", GroupId = groups[1].GroupId },
                new Student { FirstName = "qwerty3", LastName = "asdf3", GroupId = groups[0].GroupId }
            };
            await ctx.Set<Student>().AddRangeAsync(students);
            await ctx.SaveChangesAsync();

            var filter = new StudentGroupFilter { GroupName = "КТ-31-20" };

            // Act
            var studentsResult = await studentService.GetStudentsByGroupAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, studentsResult.Length);
        }
    }
 }
