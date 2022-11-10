using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;

namespace TMS.Business.Abstracts
{
    public interface IUserService
    {
        UserDto Register(UserRegisterRequestDto registerUser);
        LogInDto Login(UserLoginRequestDto loginUser);
    }
}
