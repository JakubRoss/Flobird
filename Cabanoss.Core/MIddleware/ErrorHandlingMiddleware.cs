using Cabanoss.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Cabanoss.Core.MIddleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ResourceNotFoundException ex)
            {
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
