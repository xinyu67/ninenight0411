namespace DI.Models
{
    public class CartModels
    {
        public Guid cart_id { get; set; }
        public Guid user_id { get; set; }
        public int cart_states { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string update_id { get; set; }
        public DateTime update_time { get; set; }
    }
}
