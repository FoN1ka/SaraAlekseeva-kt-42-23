using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Specialnost
    {
        public int SpecialnostId { get; set; }
        public string Title { get; set; }
        public long Code { get; set; }
        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
