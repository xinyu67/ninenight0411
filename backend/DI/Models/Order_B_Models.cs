namespace DI.Models
{
    public class Order_B_Models
    {
        public Guid order_id { get; set; }
        public Guid cart_id { get; set; }
        public string order_name { get; set; }
        public int order_num { get; set; }
        public int order_price { get; set; }
        public DateTime order_date { get; set; }
        public string order_picktime { get; set; }
        public bool order_pick { get; set; }
        public string order_address { get; set; }
        public string order_phone { get; set; }
        public int order_state { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }
    }
}
