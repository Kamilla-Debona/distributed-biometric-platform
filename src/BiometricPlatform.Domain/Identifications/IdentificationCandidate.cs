namespace BiometricPlatform.Domain.Identifications;

public sealed class IdentificationCandidate : Entity
{
    private IdentificationCandidate()
    {
    }

    public IdentificationCandidate(
        Guid identificationId,
        Guid personId,
        Guid subjectId,
        decimal score,
        int rank)
    {
        IdentificationId = identificationId;
        PersonId = personId;
        SubjectId = subjectId;
        Score = score;
        Rank = rank;
    }

    public Guid IdentificationId { get; private set; }

    public Guid PersonId { get; private set; }

    public Guid SubjectId { get; private set; }

    public decimal Score { get; private set; }

    public int Rank { get; private set; }
}