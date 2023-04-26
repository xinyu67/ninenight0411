namespace DI.ViewModels
{
    public class Order_B_AllViewModels
    {
        public Guid order_id { get; set; }
        public string order_name { get; set; }
        public string order_phone { get; set; }
        public int order_price { get; set; }
        public string order_picktime { get; set; }
        public bool order_pick { get; set; }
        public string order_address { get; set; }
        public int order_state { get; set; }
    }
}
