using BiometricPlatform.Application.Enrollments.CreateEnrollment;
using Microsoft.Extensions.DependencyInjection;

namespace BiometricPlatform.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateEnrollmentHandler>();

        return services;
    }
}