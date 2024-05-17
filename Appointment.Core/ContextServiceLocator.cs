using Microsoft.AspNetCore.Http;

namespace Appointment.Core;

public class ContextServiceLocator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }