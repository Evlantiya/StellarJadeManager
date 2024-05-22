namespace StellarJadeManager.Shared
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
    }

}
