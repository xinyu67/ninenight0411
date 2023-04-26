namespace DI.ViewModels
{
    public class CartAllViewModels
    {
        //cart
        public Guid cart_id { get; set; }

        //product
        public List<CartProductViewModels> product_list { get; set; }
        public int total { get; set; }
    }
}
