namespace DI.Models
{
    public class StoryModels
    {
        public Guid story_id { get; set; }
        public string story_title { get; set; }
        public string story_content { get; set; }
        public string story_img { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
