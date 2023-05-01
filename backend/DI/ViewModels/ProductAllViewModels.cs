namespace DI.ViewModels
{
    public class ProductAllViewModels
    {
        public string product_id { get; set; }
        //public string product_num { get; set; }
        public string product_name { get; set; }
        public string product_img { get; set; }
        //public Guid brand_id { get; set; }
        //public string brand_name { get; set; }
        public int product_price { get; set; }
        //public Guid place_id { get; set; }
        public string place_name { get; set; }
        public int product_ml { get; set; }

        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }


    }
}
