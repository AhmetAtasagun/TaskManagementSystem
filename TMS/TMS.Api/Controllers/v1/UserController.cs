using Microsoft.AspNetCore.Mvc;
using TMS.Api.Controllers.v1.Base;
using TMS.Business.Abstracts;
using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;

namespace TMS.Api.Controllers.v1
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public UserDto Register(UserRegisterRequestDto registerUser)
        {
            return _userService.Register(registerUser);
        }

        [HttpPost("Login")]
        public LogInDto Login(UserLoginRequestDto loginUser)
        {
            return _userService.Login(loginUser);
        }
    }
}
