namespace DI.Models
{
    public class ProductModels
    {
        public Guid product_id { get; set; }
        public string product_num { get; set; }
        public string product_name { get; set; }
        public string product_eng { get; set; }
        public bool product_like { get; set; }
        public string product_img { get; set; }
        public Guid brand_id { get; set; }
        public int product_price { get; set; }
        public Guid place_id { get; set; }
        public int product_ml { get; set; }
        public string product_content { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
