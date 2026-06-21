using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Infrastructure.Persistence.Repositories;
using BiometricPlatform.Application.Abstractions.Messaging;
using BiometricPlatform.Application.Abstractions.Storage;
using BiometricPlatform.Infrastructure.Biometrics;
using BiometricPlatform.Infrastructure.Messaging;
using BiometricPlatform.Infrastructure.Storage;

namespace BiometricPlatform.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<BiometricPlatformDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<BiometricPlatformDbContext>());

        services.AddScoped<IBiographicDataRepository, BiographicDataRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IBiometricSampleRepository, BiometricSampleRepository>();
        services.AddScoped<IObjectStorage, LocalObjectStorage>();
        services.AddScoped<IMessageBus, InMemoryMessageBus>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IBiometricTemplateRepository, BiometricTemplateRepository>();
        services.AddScoped<IBiometricEngine, FakeBiometricEngine>();
        
        return services;
    }
}