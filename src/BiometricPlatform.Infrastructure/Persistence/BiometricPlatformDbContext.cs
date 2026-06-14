using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Enrollments;
using BiometricPlatform.Domain.Identifications;
using BiometricPlatform.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace BiometricPlatform.Infrastructure.Persistence;

public sealed class BiometricPlatformDbContext : DbContext, IUnitOfWork
{
    public BiometricPlatformDbContext(
        DbContextOptions<BiometricPlatformDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();

    public DbSet<Person> Persons => Set<Person>();

    public DbSet<BiographicData> BiographicData => Set<BiographicData>();

    public DbSet<Gallery> Galleries => Set<Gallery>();

    public DbSet<Subject> Subjects => Set<Subject>();

    public DbSet<BiometricSample> BiometricSamples => Set<BiometricSample>();

    public DbSet<BiometricTemplate> BiometricTemplates => Set<BiometricTemplate>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    public DbSet<Identification> Identifications => Set<Identification>();

    public DbSet<IdentificationCandidate> IdentificationCandidates => Set<IdentificationCandidate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BiometricPlatformDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}