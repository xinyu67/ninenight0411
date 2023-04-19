namespace DI.ViewModels
{
    public class StoryUpdateViewModels
    {
        public Guid story_id { get; set; }
        public string story_title { get; set; }
        public string story_content { get; set; }
        public IFormFile story_img { get; set; }
    }
}
