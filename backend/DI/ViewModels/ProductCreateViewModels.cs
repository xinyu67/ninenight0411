namespace DI.ViewModels
{
    public class ProductCreateViewModels
    {
        public string product_num { get; set; }
        public string product_name { get; set; }
        public string product_eng { get; set; }
        public IFormFile product_img { get; set; }
        public Guid brand_id { get; set; }
        public int product_price { get; set; }
        public Guid place_id { get; set; }
        public int product_ml { get; set; }
        public string product_content { get; set; }
    }
}
