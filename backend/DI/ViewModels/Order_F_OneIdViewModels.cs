namespace DI.ViewModels
{
    public class Order_F_OneIdViewModels
    {
        public Guid order_id { get; set; }
        public Guid cart_id { get; set; }
        public string product_img { get; set; }
        public string product_name { get; set; }
        public int product_price { get; set; }
        public int cart_product_amount { get; set; }
        public int money { get; set; }
        public int order_price { get; set; }
    }
}
