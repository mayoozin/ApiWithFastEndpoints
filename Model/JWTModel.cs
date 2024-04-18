namespace ApiWithFastEndpoints.Model
{
    public class JWTModel
    {
        public string JwtToken { get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TokenExpiry { get; set; }

    }
}
