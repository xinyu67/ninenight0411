namespace DI.ViewModels
{
    public class Order_B_ID_ViewModels
    {
        public Guid cart_id { get; set; }
        public Guid order_id { get; set; }
        public string user_account { get; set; }
        public bool order_pick { get; set; }
        public string order_address { get; set; }
        public string order_date { get; set; }
        public Guid product_id { get; set; }
        public string product_name { get; set; }
        public int product_ml { get; set; }
        public int cart_product_amount { get; set; }
        public int product_price { get; set; }
        public int order_price { get; set; }
    }
}
