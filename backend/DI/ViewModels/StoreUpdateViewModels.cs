namespace DI.ViewModels
{
    public class StoreUpdateViewModels
    {
        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_email { get; set; }
        public string store_phone { get; set; }
        public string store_time { get; set; }
        public IFormFile? store_img { get; set; }
    }
}
