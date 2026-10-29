namespace WebApplication1.Models
{
    public class UpdateGroupDto
    {
        public int GroupId { get; set; }
        public string NewName { get; set; }
        public int NewCourse { get; set; }
        public int NewSpecialnostId { get; set; }
    }
}
