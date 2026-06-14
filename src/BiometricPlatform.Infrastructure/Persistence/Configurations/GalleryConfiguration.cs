using BiometricPlatform.Domain.Biometrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class GalleryConfiguration
    : IEntityTypeConfiguration<Gallery>
{
    public void Configure(EntityTypeBuilder<Gallery> builder)
    {
        builder.ToTable("galleries");

        builder.HasKey(gallery => gallery.Id);
        
        builder.Property(gallery => gallery.Id)
            .HasColumnName("id");

        builder.Property(gallery => gallery.ClientId)
            .HasColumnName("client_id");

        builder.Property(gallery => gallery.Name)
            .HasColumnName("name")
            .HasMaxLength(200);

        builder.Property(gallery => gallery.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(gallery => gallery.CreatedAtUtc)
            .HasColumnName("created_at_utc");
    }
}