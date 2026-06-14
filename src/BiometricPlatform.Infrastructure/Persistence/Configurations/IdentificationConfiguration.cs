using BiometricPlatform.Domain.Identifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class IdentificationConfiguration
    : IEntityTypeConfiguration<Identification>
{
    public void Configure(EntityTypeBuilder<Identification> builder)
    {
        builder.ToTable("identifications");

        builder.HasKey(identification => identification.Id);

        builder.Property(identification => identification.Id)
            .HasColumnName("id");

        builder.Property(identification => identification.ClientId)
            .HasColumnName("client_id")
            .IsRequired();

        builder.Property(identification => identification.GalleryId)
            .HasColumnName("gallery_id")
            .IsRequired();

        builder.Property(identification => identification.ProbeSampleId)
            .HasColumnName("probe_sample_id")
            .IsRequired();

        builder.Property(identification => identification.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(identification => identification.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(identification => identification.CompletedAtUtc)
            .HasColumnName("completed_at_utc");
    }
}