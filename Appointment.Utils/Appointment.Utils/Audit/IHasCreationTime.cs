namespace Appointment.Utils.Audit;

public interface IHasCreationTime
{
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    DateTime CreationTime { get; set; }
}