using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAuthentication.Data.Context
{
    public class AuthenticationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
    {
        public AuthenticationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();
            // this is used for design time use when building migrations

            return new AuthenticationDbContext(optionsBuilder.Options);
        }
    }
}
