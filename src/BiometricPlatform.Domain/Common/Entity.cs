public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid Id { get; protected set; }

    public DateTime CreatedAtUtc { get; protected set; }
}