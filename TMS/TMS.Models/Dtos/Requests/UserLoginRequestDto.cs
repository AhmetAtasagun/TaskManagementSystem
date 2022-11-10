using TMS.Core.Abstracts;

namespace TMS.Models.Dtos.Requests
{
    public class UserLoginRequestDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
