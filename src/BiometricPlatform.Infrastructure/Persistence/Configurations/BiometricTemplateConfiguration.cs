using BiometricPlatform.Domain.Biometrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class BiometricTemplateConfiguration
    : IEntityTypeConfiguration<BiometricTemplate>
{
    public void Configure(EntityTypeBuilder<BiometricTemplate> builder)
    {
        builder.ToTable("biometric_templates");

        builder.HasKey(template => template.Id);

        builder.Property(template => template.Id)
            .HasColumnName("id");

        builder.Property(template => template.SubjectId)
            .HasColumnName("subject_id")
            .IsRequired();

        builder.Property(template => template.BiometricSampleId)
            .HasColumnName("biometric_sample_id")
            .IsRequired();

        builder.Property(template => template.VectorId)
            .HasColumnName("vector_id")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(template => template.ModelVersion)
            .HasColumnName("model_version")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(template => template.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.HasIndex(template => template.VectorId)
            .IsUnique();
    }
}