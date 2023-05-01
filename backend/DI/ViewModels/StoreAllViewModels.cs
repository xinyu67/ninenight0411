namespace DI.ViewModels
{
    public class StoreAllViewModels
    {
        public Guid store_id { get; set; }
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_email { get; set; }
        public string store_phone { get; set; }
        public string store_time { get; set; }
        public string store_img { get; set; }

        public DateTime create_time { get; set; }
    }
}
