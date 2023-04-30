namespace DI.Security
{
    public class JwtObject
    {
        public string user_account { get; set; }
        public string Role { get; set; }
        // 到期時間
        public string Expire { get; set; }
    }
}
