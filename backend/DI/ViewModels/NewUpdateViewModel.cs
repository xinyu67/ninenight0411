namespace DI.ViewModels
{
    public class NewUpdateViewModel
    {
        public Guid new_id { get; set; }
        public string new_title { get; set; }
        public DateOnly new_startdate { get; set; }
        public DateOnly new_enddate { get; set; }
        public string new_content { get; set; }
        public IFormFile new_img { get; set; }
    }
}
