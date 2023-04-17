namespace DI.Models
{
    public class NewModels
    {
        public Guid new_id { get; set; }
        public string new_title { get; set;}
        public DateOnly new_startdate { get; set;}
        public DateOnly new_enddate { get; set;}
        public string new_content { get; set; }
        public string new_img { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
