using BiometricPlatform.Domain.Biometrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("subjects");

        builder.HasKey(subject => subject.Id);

        builder.Property(subject => subject.Id)
            .HasColumnName("id");

        builder.Property(subject => subject.PersonId)
            .HasColumnName("person_id")
            .IsRequired();

        builder.Property(subject => subject.GalleryId)
            .HasColumnName("gallery_id")
            .IsRequired();

        builder.Property(subject => subject.ExternalSubjectId)
            .HasColumnName("external_subject_id")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(subject => subject.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(subject => subject.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.HasIndex(subject => new
        {
            subject.PersonId,
            subject.GalleryId
        }).IsUnique();
    }
}