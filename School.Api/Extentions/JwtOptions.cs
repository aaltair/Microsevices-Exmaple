namespace School.Api.Extentions
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiryDay { get; set; }
        public string Issuer { get; set; }
    }
}