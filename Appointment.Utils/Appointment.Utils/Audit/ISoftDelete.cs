namespace Appointment.Utils.Audit;

/// <summary>
/// This interface is implemented by entities that is wanted to store deletion information (who and when created).
/// Deletion time and deleter user are automatically set when saving <see cref="Entity"/> to database.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Id of the deleter user of this entity.
    /// </summary>
    long? DeleterUserId { get; set; }

    DateTime? DeletionTime { get; set; }

    bool IsDeleted {get; set;}
}

/// <summary>
/// Adds navigation properties to <see cref="IDeletionAudited"/> interface for user.
/// </summary>
/// <typeparam name="TUser">Type of the user</typeparam>
public interface ISoftDelete<TUser> : ISoftDelete
    where TUser : IEntity<long>
{
    /// <summary>
    /// Reference to the deleter user of this entity.
    /// </summary>
    TUser DeleterUser { get; set; }
}