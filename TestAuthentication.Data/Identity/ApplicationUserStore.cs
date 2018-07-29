using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestAuthentication.Data.Context;
using TestAuthentication.Data.Models;

namespace TestAuthentication.Data.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, AuthenticationDbContext, int>
    {
        public ApplicationUserStore(AuthenticationDbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> FindByTemporaryLoginTokenAsync(string token)
        {
            var tokenGuid = Guid.Empty;
            var bytes = Convert.FromBase64String(token);
            tokenGuid = new Guid(bytes);

            var user = await Context.Users
                .Where(x =>
                    x.TemporaryTwoFactorLoginToken.Equals(tokenGuid)
                    && x.DateTemporaryTwoFactorLoginTokenExpiresUtc > DateTime.UtcNow
                )
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
