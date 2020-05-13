namespace AuthService.API.Application.Models.Authentication
{
    public class TokenInfo
    {
        public TokenInfo(string userId, string userName, string userEmail, int expiresIn)
        {
            User = new UserInfo { Id = userId, Name = userName, Email = userEmail };
            ExpiresIn = expiresIn;
        }

        public UserInfo User { get; private set; }
        public int ExpiresIn { get; set; }
    }
}
