using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        [JsonIgnore]
        public ICollection<Discipline> Grades { get; set; } = new List<Discipline>();
    }
}
