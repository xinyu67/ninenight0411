using System.ComponentModel.DataAnnotations;

namespace DI.Models
{
    public class User
    {
        [Key]
        public Guid user_id { get; set; } = Guid.NewGuid();
        public string user_account { get; set; }


        // 密碼
        //[DisplayName("密碼:")]
        // [Required(ErrorMessage = "請輸入密碼")]
        //  [StringLength(20, MinimumLength = 8, ErrorMessage = "帳號長度需介於8-20字元")]
        public string user_pwd { get; set; }
        // 姓名

        public string user_name { get; set; }
        //性別
        public int user_gender { get; set; }
        //生日
        public DateTime user_birthday { get; set; }
        // 電子信箱
        public string user_email { get; set; }
        // 信箱驗證碼
        public string user_authcode { get; set; }
        public string user_phone { get; set; }
        public string user_address { get; set; }
        // 管理者
        public bool user_level { get; set; }
        public bool isdel { get; set; } = false;

        public bool user_start { get; set; }
    }
}
