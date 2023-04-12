namespace DI.Models
{
    public class BrandModels
    {
        public Guid brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_eng { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
