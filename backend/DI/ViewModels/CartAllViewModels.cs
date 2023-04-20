namespace DI.ViewModels
{
    public class CartAllViewModels
    {
        //cart
        public Guid cart_id { get; set; }

        //cart_product
        //public Guid cart_product_id { get; set; }
        //public Guid product_id { get; set; }
        public int cart_product_amount { get; set; }

        //product
        public List<ProductCartViewModels> product_list { get; set; }

        public int money { get; set; }
        public int total { get; set; }
    }
}
