using BobrVerse.Auth.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BobrVerse.Auth.Middlewares
{
    public class MainAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;

        public MainAuthMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!await _authService.ValidateRequestAsync(context))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}