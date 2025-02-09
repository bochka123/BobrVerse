﻿using BobrVerse.Auth.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BobrVerse.Auth.Attributes
{
    public class MainAuthFilter(IHttpContextAccessor httpContextAccessor, IAuthService authService) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (!await authService.ValidateRequestAsync(httpContext))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

    public class MainAuthAttribute : TypeFilterAttribute
    {
        public MainAuthAttribute() : base(typeof(MainAuthFilter))
        {
        }
    }
}
