using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string? GetJwt(this IHttpContextAccessor httpContextAccessor)
        {
            string? authHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization;
            string? jwtToken = authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : null;
            return jwtToken;
        }
    }
}
