namespace StellarJadeManager.Shared
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Name { get; set;}
        public string Email { get; set;}

        public DateTime LastActive { get; set; }

        public string Token { get; set;}

        public DateTime Expiration { get; set;}
    }
}