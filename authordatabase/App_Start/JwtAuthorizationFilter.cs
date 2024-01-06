using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace emptables.App_Start
{
    public class JwtAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader == null || authHeader.Scheme.ToLower() != "bearer")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Missing or invalid Authorization header");
                return;
            }

            var token = authHeader.Parameter;

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("73C2FD55D4152D973A5B671C6114DCF6535B443BE2748B9197312B2FB94BB827DEE2942B4238C92529C5F28217"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "GT",
                    ValidAudience = "Dev",
                    IssuerSigningKey = securityKey
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                actionContext.RequestContext.Principal = principal;
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
                return;
            }
        }
    }
}