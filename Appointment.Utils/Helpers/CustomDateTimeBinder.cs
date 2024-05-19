using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Appointment.Utils.Helpers;

public class CustomDateTimeBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateTime))
        {
            bindingContext.Result = ModelBindingResult.Success(dateTime);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid DateTime format.");
        }

        return Task.CompletedTask;
    }
}