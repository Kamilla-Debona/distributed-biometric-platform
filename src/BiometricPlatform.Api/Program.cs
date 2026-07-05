using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.DependencyInjection;
using BiometricPlatform.Application.Enrollments.ProcessEnrollment;
using BiometricPlatform.Application.Identifications.ProcessIdentification;
using BiometricPlatform.Infrastructure.DependencyInjection;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(options =>
{
    options.UseRuntimeCompilation();

    options.Discovery.IncludeAssembly(typeof(ProcessEnrollmentHandler).Assembly);
    options.Discovery.IncludeAssembly(typeof(ProcessIdentificationHandler).Assembly);

    options.CodeGeneration.AlwaysUseServiceLocationFor<IEnrollmentRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<IPersonRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<IBiometricSampleRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<ISubjectRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<IBiometricTemplateRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<IIdentificationRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<IIdentificationCandidateRepository>();
    options.CodeGeneration.AlwaysUseServiceLocationFor<IUnitOfWork>();
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
