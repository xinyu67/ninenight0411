namespace DI.Models
{
    public class PlaceModels
    {
        public Guid place_id { get; set; }
        public string place_name { get; set; }
        public string place_eng { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
