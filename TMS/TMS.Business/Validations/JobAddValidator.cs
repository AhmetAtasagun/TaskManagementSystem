using FluentValidation;
using TMS.Models.Dtos.Requests;

namespace TMS.Business.Validations
{
    public class JobAddValidator : AbstractValidator<JobAddRequestDto>
    {
        public JobAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.JobContent).NotEmpty();
        }
    }
}
