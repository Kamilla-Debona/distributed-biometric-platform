using BiometricPlatform.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments");

        builder.HasKey(enrollment => enrollment.Id);

        builder.Property(enrollment => enrollment.Id)
            .HasColumnName("id");

        builder.Property(enrollment => enrollment.PersonId)
            .HasColumnName("person_id")
            .IsRequired();

        builder.Property(enrollment => enrollment.GalleryId)
            .HasColumnName("gallery_id")
            .IsRequired();

        builder.Property(enrollment => enrollment.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(enrollment => enrollment.FailureReason)
            .HasColumnName("failure_reason")
            .HasMaxLength(500);

        builder.Property(enrollment => enrollment.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(enrollment => enrollment.CompletedAtUtc)
            .HasColumnName("completed_at_utc");
    }
}