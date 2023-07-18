using Identity.Domain.Services;
using Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Domain.Filters;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        try
        {
            var administratorToken = new AdministratorToken(new TokenJwt());
            var adm = administratorToken.TokenToAdm(token);

            if (adm == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // context.HttpContext.User = adm;
        }
        catch
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
