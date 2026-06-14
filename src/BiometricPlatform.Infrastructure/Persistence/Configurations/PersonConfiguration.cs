using BiometricPlatform.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder.HasKey(person => person.Id);

        builder.Property(person => person.Id)
            .HasColumnName("id");

        builder.Property(person => person.ClientId)
            .HasColumnName("client_id")
            .IsRequired();

        builder.Property(person => person.BiographicDataId)
            .HasColumnName("biographic_data_id")
            .IsRequired();

        builder.Property(person => person.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(person => person.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(person => person.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");
    }
}