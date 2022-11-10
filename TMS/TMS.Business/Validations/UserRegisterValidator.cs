using FluentValidation;
using TMS.Models.Dtos.Requests;

namespace TMS.Business.Validations
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterRequestDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4).MaximumLength(10);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
        }
    }
}
