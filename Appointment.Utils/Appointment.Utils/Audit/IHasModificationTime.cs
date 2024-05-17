namespace Appointment.Utils.Audit;

public interface IHasModificationTime
{
    DateTime? LastModificationTime { get; set; }
}