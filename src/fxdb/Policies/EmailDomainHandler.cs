using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
namespace fxdb.Policies {
    public class EmailDomainHandler : AuthorizationHandler<EmailDomainRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailDomainRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated) return Task.CompletedTask;
            MailAddress email;
            try {
                email = new MailAddress(context.User.Identity.Name);
            } catch {
                return Task.CompletedTask;
            }
            foreach(var domain in requirement.AuthorizedDomains) {
                if (email.Host == domain) context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}