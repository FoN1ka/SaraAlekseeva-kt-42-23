using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();

    }
}
