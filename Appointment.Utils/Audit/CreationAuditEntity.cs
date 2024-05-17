namespace Appointment.Utils.Audit;

/// <summary>
/// A shortcut of <see cref="CreationAuditEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public abstract class CreationAuditEntity : AuditEntity<int>, IEntity, IHasCreationTime, ICreationAudited
{
}

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class CreationAuditEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationTime, ICreationAudited
{
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    public virtual DateTime CreationTime { get; set; }

    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual long? CreatorUserId { get; set; }
}