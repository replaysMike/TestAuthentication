using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TestAuthentication.Data.Models;

namespace TestAuthentication.Data.Context
{
    public partial class AuthenticationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public string ConnectionString { get; set; }


        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
            /*this is used for the designtimefactory, when running migrations*/
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            var configValue = config[$"ConnectionStrings:AuthenticationConnection"];

            if (string.IsNullOrEmpty(configValue))
                throw new Exception($"There are no configuration strings named AuthenticationConnection available!");

            ConnectionString = configValue;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
                optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Add your customizations after calling base.OnModelCreating(builder);

            // use a custom schema for Identity
            builder.Entity<ApplicationUser>().ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("UserId");
            builder.Entity<ApplicationRole>().ToTable("Roles", "dbo").Property(p => p.Id).HasColumnName("RoleId");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", "dbo").Property(p => p.Id).HasColumnName("ClaimId");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", "dbo").Property(p => p.Id).HasColumnName("RoleClaimId");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", "dbo");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoleAssignments", "dbo");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLoginAssignments", "dbo");

            // use Fluent to set some default server values
            builder.Entity<ApplicationUser>().Property(p => p.UserGlobalId).IsRequired().HasDefaultValueSql("newid()");
            builder.Entity<ApplicationUser>().Property(p => p.DateCreatedUtc).IsRequired().HasDefaultValueSql("getutcdate()");
            builder.Entity<ApplicationUser>().Property(p => p.DateModifiedUtc).IsRequired().HasDefaultValueSql("getutcdate()");
        }
    }
}
