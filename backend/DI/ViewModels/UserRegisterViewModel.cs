namespace DI.ViewModels
{
    public class UserRegisterViewModel
    {
        public string user_account { get; set; }
        public string user_pwd { get; set; }
        public string user_name { get; set; }
        //性別
        public bool user_gender { get; set; }
        //生日
        public DateTime user_birthday { get; set; }
        // 電子信箱
        public string user_email { get; set; }
        public string user_phone { get; set; }
        public string user_address { get; set; }
    }
}
