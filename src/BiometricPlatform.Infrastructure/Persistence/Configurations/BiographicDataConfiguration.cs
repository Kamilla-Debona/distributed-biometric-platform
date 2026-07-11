using BiometricPlatform.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class BiographicDataConfiguration
    : IEntityTypeConfiguration<BiographicData>
{
    public void Configure(EntityTypeBuilder<BiographicData> builder)
    {
        builder.ToTable("biographic_data");

        builder.HasKey(biographicData => biographicData.Id);

        builder.Property(biographicData => biographicData.Id)
            .HasColumnName("id");

        builder.Property(biographicData => biographicData.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(biographicData => biographicData.Document)
            .HasColumnName("document")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(biographicData => biographicData.DateOfBirth)
            .HasColumnName("date_of_birth");

        builder.Property(biographicData => biographicData.CreatedAtUtc)
            .HasColumnName("created_at_utc");
    }
}