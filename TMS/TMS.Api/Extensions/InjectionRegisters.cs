using FluentValidation;
using FluentValidation.AspNetCore;
using TMS.Business.Abstracts;
using TMS.Business.Services;
using TMS.Business.Validations;
using TMS.Core.Abstracts;
using TMS.Data;
using TMS.Models.Dtos.Requests;

namespace TMS.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void InjectionRegisters(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IUserService, UserService>();

        // Validations
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<UserRegisterRequestDto>, UserRegisterValidator>();
        services.AddScoped<IValidator<UserLoginRequestDto>, UserLoginValidator>();
    }
}
