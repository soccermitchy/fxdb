using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace fxdb.Policies {
    public class EmailDomainRequirement : IAuthorizationRequirement {
        public List<string> AuthorizedDomains { get; set; }
        public EmailDomainRequirement(List<string> domains) {
            AuthorizedDomains = domains;
        }
    }
}