using Microsoft.Extensions.Options;
using TMS.Business.Abstracts;
using TMS.Business.Helpers;
using TMS.Business.Validations;
using TMS.Core.Abstracts;
using TMS.Core.Helpers.EasyMapper;
using TMS.Models;
using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;
using TMS.Models.Entites;

namespace TMS.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserToken> _userTokenRepository;
        private readonly TokenSettings _tokenSettings;

        public UserService(IRepository<User> userRepository, IOptions<TokenSettings> tokenSettings, IRepository<UserToken> userTokenRepository)
        {
            _userRepository = userRepository;
            _tokenSettings = tokenSettings.Value;
            _userTokenRepository = userTokenRepository;
        }

        public LogInDto Login(UserLoginRequestDto loginUser)
        {
            var user = _userRepository.Get(x => x.Email == loginUser.Email);
            UserLoginValidator.Validate(user, loginUser);
            var expireDate = DateTime.UtcNow.AddMinutes(_tokenSettings.AccessTokenExpiration);
            var token = TokenHelper.CreateToken(user, _tokenSettings, expireDate);
            _userTokenRepository.Add(new UserToken
            {
                Token = token,
                TokenExpireDate = expireDate,
                UserId = user.Id,
                User = user
            });
            return new LogInDto
            {
                Email = loginUser.Email,
                Token = token,
            };
        }

        public UserDto Register(UserRegisterRequestDto registerUser)
        {
            var user = registerUser.ToMap<User>();
            var salt = HashingHelper.GenerateSecurityCode();
            user.Password = HashingHelper.HashUse(user.Password, salt);
            user.PasswordSalt = salt;
            return _userRepository.Add(user).ToMap<UserDto>();
        }
    }
}
