using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Appointment.Utils.Helpers;

public class CustomTimeSpanBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (TimeSpan.TryParseExact(value, @"hh\:mm", null, out var timeSpan))
        {
            bindingContext.Result = ModelBindingResult.Success(timeSpan);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid TimeSpan format.");
        }

        return Task.CompletedTask;
    }
}