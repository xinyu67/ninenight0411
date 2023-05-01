namespace DI.ViewModels
{
    public class Order_F_CreateViewModels
    {
        public Guid cart_id { get; set; }
        public Guid user_id { get; set; }
        //public int order_num { get; set; }
        public int order_price { get; set; }
        public string order_name { get; set; }
        public bool order_pick { get; set;}
        public string order_address { get; set; }
        public string order_phone { get; set; }
        public string order_picktime { get; set;}
    }
}
