using BiometricPlatform.Application.Enrollments.CreateEnrollment;
using BiometricPlatform.Application.Enrollments.ProcessEnrollment;
using BiometricPlatform.Application.Identifications.CreateIdentification;
using Microsoft.Extensions.DependencyInjection;

namespace BiometricPlatform.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateEnrollmentHandler>();
        services.AddScoped<ProcessEnrollmentHandler>();
        services.AddScoped<CreateIdentificationHandler>();

        return services;
    }
}