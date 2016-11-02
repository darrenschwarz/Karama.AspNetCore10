using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace SwashbuckleExample.AuthorizationRequirements
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>//TODO: Refactor this is just for spike
    {
        //private readonly IHttpContextAccessor _ihttpContextAccessor;

        public RoleHandler()
        {

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if(context.User != null)
            {
                if (context.User.IsInRole(requirement.Role))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //_ihttpContextAccessor.HttpContext.Response = new UnauthorizedAccessException($"{requirement.Role}",null);
                    //context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}