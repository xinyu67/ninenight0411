namespace DI.Models
{
    public class UserModels
    {
        public Guid user_id { get; set; }
        public string user_account { get; set; }
        public string user_pwd { get; set;}
        public string user_name { get; set; }
        public int? user_gender { get; set; }
        public string? user_birthday { get; set; }
        public string? user_email { get; set; }
        public string? user_authcode { get; set; }
        public string? user_phone { get; set; }
        public string? user_address { get; set; }
        public bool user_level { get; set; }
        public int user_start { get; set; }
        public bool isdel { get; set; }
        public string create_id { get; set; }
        public DateTime create_time { get; set; }
        public string? update_id { get; set; }
        public DateTime? update_time { get; set; }

    }
}
