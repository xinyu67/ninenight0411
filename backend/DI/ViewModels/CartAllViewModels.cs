namespace DI.ViewModels
{
    public class CartAllViewModels
    {
        //cart
        public Guid cart_id { get; set; }

        //cart_product
        //public Guid cart_product_id { get; set; }

        //product
        public List<ProductCartViewModels> product_list { get; set; }
        //public string product_name { get; set; }
        //public string product_img { get; set; }
        //public int product_price { get; set; }
        public int total { get; set; }
    }
}
