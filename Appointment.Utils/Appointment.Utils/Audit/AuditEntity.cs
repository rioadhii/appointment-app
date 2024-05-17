namespace Appointment.Utils.Audit;

/// <summary>
/// A shortcut of <see cref="AuditEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public abstract class AuditEntity : AuditEntity<int>, IEntity, IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited
{

}

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class AuditEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited
{
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    public virtual DateTime CreationTime { get; set; }

    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual long? CreatorUserId { get; set; }

    /// <summary>
    /// Last modification date of this entity.
    /// </summary>
    public virtual DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// Last modifier user of this entity.
    /// </summary>
    public virtual long? LastModifierUserId { get; set; }
}