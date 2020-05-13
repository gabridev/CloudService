namespace AuthService.API.Application.Models.Authentication
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
    }
}
