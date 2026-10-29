using WebApplication1.Models;

namespace SaraAlekseevaKT_42_23.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT4223_True()
        {
            var testGroup = new Group
            {
                Name="КТ-42-23"
            };

            var result = testGroup.isValidGroupName();

            Assert.True(result);
        }

        [Fact]
        public void IsValidGroupName_InvalidName_False()
        {
            // Arrange
            var testGroup = new Group { Name = "Группа 1" };

            // Act
            var result = testGroup.isValidGroupName();

            // Assert
            Assert.False(result);
        }
    }
}
