using BobrVerse.Common.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace BobrVerse.Api.Hubs
{
    public class AuthHubFilter : IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(
            HubInvocationContext invocationContext,
            Func<HubInvocationContext, ValueTask<object>> next
        )
        {
            var expiryClaim = invocationContext.Context.User.Claims.FirstOrDefault(x => x.Type == "exp").Value;
            //if (string.IsNullOrEmpty(expiryClaim) || !long.TryParse(expiryClaim, out var expiryUnixTimestamp))
            //{
            //    throw new BobrException("Invalid or missing 'exp' claim.");
            //}

            //var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryUnixTimestamp);

            //if (DateTimeOffset.UtcNow >= expiryDate)
            //{
            //    throw new BobrException("auth_expired");
            //}

            return await next(invocationContext);
        }

        public Task OnConnectedAsync(
            HubLifetimeContext context,
            Func<HubLifetimeContext, Task> next
        )
        {
            return next(context);
        }

        public Task OnDisconnectedAsync(
            HubLifetimeContext context,
            Exception exception,
            Func<HubLifetimeContext, Exception, Task> next
        )
        {
            return next(context, exception);
        }
    }
}
