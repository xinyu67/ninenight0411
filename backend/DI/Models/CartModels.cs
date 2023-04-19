namespace DI.Models
{
    public class CartModels
    {
        //cart
        public Guid cart_id { get; set; }
        public Guid user_id { get; set; }
        public int cart_states { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string update_id { get; set; }
        public DateTime update_time { get; set; }

        //cart_product
        public Guid cart_product_id { get; set; }
        public Guid product_id { get; set; }
        public int cart_product_amount { get; set;}

        //user
        public string user_account { get; set;}
        public string user_name { get; set; }

        //product
        public string product_name { get; set; }
        public string product_img { get; set;}
        public int product_price { get; set;}
    }
}
