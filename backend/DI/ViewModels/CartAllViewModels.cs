namespace DI.ViewModels
{
    public class CartAllViewModels
    {
        //cart
        public Guid cart_id { get; set; }

        //cart_product
        //public Guid cart_product_id { get; set; }
       // public Guid product_id { get; set; }
        public int cart_product_amount { get; set; }

        //public List<ProductIdViewModels> product_list { get; set; }

        //product
        public string product_name { get; set; }
        public string product_img { get; set; }
        public int product_price { get; set; }

        public int money { get; set; }
        public int total { get; set; }
    }
}
