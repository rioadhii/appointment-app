using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Appointment.Utils.Filters;

public class LowercaseControllerConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.ControllerName = controller.ControllerName.ToLower();
    }
}