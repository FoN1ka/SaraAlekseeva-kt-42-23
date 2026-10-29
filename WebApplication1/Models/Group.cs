using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }
        public int Course {  get; set; }
        public int SpecialnostId { get; set; }
        public Specialnost Specialnost { get; set; }

        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
