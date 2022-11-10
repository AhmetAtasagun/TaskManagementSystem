namespace TMS.Models.Dtos
{
    public class LogInDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
