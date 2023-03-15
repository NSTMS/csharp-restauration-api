using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace restAPI.Authorization
{
    public class MiniumumAgeRequireMentHandler : AuthorizationHandler<MiniumumAgeRequireMent>
    {
        public override Task HandleRequirementAsync(AuthorizationHandlerContext ctx, MiniumumAgeRequireMent req)
        {
            var dateOfBirth = DateTime.Parse(ctx.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            if()
            return Task.CompletedTask;
        }
    }
}
