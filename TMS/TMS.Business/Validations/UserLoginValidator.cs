using FluentValidation;
using TMS.Business.Helpers;
using TMS.Core.Abstracts;
using TMS.Models.Dtos.Requests;
using TMS.Models.Entites;
using TMS.Models.Exceptions;

namespace TMS.Business.Validations
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequestDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4).MaximumLength(200);
        }

        public static void Validate(User user, UserLoginRequestDto userLoginRequestDto)
        {
            if (userLoginRequestDto == null)
                throw new NullReferenceException($"{nameof(userLoginRequestDto)} is null");
            if (user != null && HashingHelper.CheckPassword(user, userLoginRequestDto.Password)) return;
            else throw new AppException($"Mail Adresi veya şifre Hatalı!");
        }
    }
}
