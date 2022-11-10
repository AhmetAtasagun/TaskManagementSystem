using TMS.Core.Abstracts;

namespace TMS.Models.Dtos.Requests
{
    public class UserRegisterRequestDto : IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
