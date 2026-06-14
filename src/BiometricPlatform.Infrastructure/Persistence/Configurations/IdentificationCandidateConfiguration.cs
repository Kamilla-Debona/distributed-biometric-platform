using BiometricPlatform.Domain.Identifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiometricPlatform.Infrastructure.Persistence.Configurations;

public sealed class IdentificationCandidateConfiguration
    : IEntityTypeConfiguration<IdentificationCandidate>
{
    public void Configure(EntityTypeBuilder<IdentificationCandidate> builder)
    {
        builder.ToTable("identification_candidates");

        builder.HasKey(candidate => candidate.Id);

        builder.Property(candidate => candidate.Id)
            .HasColumnName("id");

        builder.Property(candidate => candidate.IdentificationId)
            .HasColumnName("identification_id")
            .IsRequired();

        builder.Property(candidate => candidate.PersonId)
            .HasColumnName("person_id")
            .IsRequired();

        builder.Property(candidate => candidate.SubjectId)
            .HasColumnName("subject_id")
            .IsRequired();

        builder.Property(candidate => candidate.Score)
            .HasColumnName("score")
            .HasPrecision(10, 6)
            .IsRequired();

        builder.Property(candidate => candidate.Rank)
            .HasColumnName("rank")
            .IsRequired();

        builder.Property(candidate => candidate.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.HasIndex(candidate => new
        {
            candidate.IdentificationId,
            candidate.Rank
        }).IsUnique();
    }
}