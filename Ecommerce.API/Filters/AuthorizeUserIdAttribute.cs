using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeUserIdAttribute : TypeFilterAttribute
{
    public AuthorizeUserIdAttribute() : base(typeof(AuthorizeUserIdFilter))
    {
    }

    private class AuthorizeUserIdFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var loggedInUserId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var requestedUserId = context.RouteData.Values["userId"]?.ToString();

            if (loggedInUserId != requestedUserId)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
