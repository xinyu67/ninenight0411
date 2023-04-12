namespace DI.ViewModels
{
    public class ProductUpdateViewModel
    {
        public Guid product_id { get; set; }
        public string product_name { get; set; }
        public string product_eng { get; set; }
        public string product_img { get; set; }
        public int product_price { get; set; }
        public Guid place_id { get; set; }
        public int product_ml { get; set; }
        public string product_content { get; set; }
        //public string update_id { get; set; }
        //public DateTime update_time { get; set; }
    }
}
