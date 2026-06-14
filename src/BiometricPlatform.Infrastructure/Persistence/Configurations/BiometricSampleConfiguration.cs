using BiometricPlatform.Domain.Biometrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class BiometricSampleConfiguration
    : IEntityTypeConfiguration<BiometricSample>
{
    public void Configure(EntityTypeBuilder<BiometricSample> builder)
    {
        builder.ToTable("biometric_samples");

        builder.HasKey(sample => sample.Id);
        
        builder.Property(sample => sample.Id)
            .HasColumnName("id");

        builder.Property(sample => sample.PersonId)
            .HasColumnName("person_id");

        builder.Property(sample => sample.EnrollmentId)
            .HasColumnName("enrollment_id");

        builder.Property(sample => sample.Type)
            .HasColumnName("type")
            .HasConversion<string>();

        builder.Property(sample => sample.StoragePath)
            .HasColumnName("storage_path")
            .HasMaxLength(500);

        builder.Property(sample => sample.QualityScore)
            .HasColumnName("quality_score");

        builder.Property(sample => sample.CreatedAtUtc)
            .HasColumnName("created_at_utc");
    }
}