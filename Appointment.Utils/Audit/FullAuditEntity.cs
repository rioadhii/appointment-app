namespace Appointment.Utils.Audit;

/// <summary>
/// A shortcut of <see cref="FullAuditEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public abstract class FullAuditEntity : FullAuditEntity<int>, IEntity, IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, ISoftDelete
{

}

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class FullAuditEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, ISoftDelete
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

    /// <summary>
    /// Last deletion date of this entity.
    /// </summary>
    public virtual DateTime? DeletionTime { get; set; }

    /// <summary>
    /// Last deleter user of this entity.
    /// </summary>
    public virtual long? DeleterUserId { get; set; }

    /// <summary>
    /// Deleted flag of this entity.
    /// </summary>
    public bool IsDeleted { get; set; }
}