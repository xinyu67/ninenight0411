namespace DI.Models
{
    public class StoreModels
    {
        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_phone { get; set; }
        public string store_time { get; set; }
        public string store_img { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
