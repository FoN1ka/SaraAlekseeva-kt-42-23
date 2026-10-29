using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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


        public bool isValidGroupName()
        {
            return Regex.Match(Name, @"^[A-ZА-Я]{2}-\d{2}-\d{2}$").Success;
        }
    }
}
