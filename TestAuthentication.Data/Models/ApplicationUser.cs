using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAuthentication.Data.Models
{
    [Table("Users")]
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
        }

        /// <summary>
        /// Primary key identifer for user that can be used across databases
        /// </summary>
        [Required]
        public Guid UserGlobalId { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A random key to be used for two-factor authentication
        /// Generated once upon registration
        /// </summary>
        public string TwoFactorSecret { get; set; }

        /// <summary>
        /// A 6 digit pin-number to be used for two-factor authentication.
        /// Regenerates on every usage
        /// </summary>
        public int TwoFactorPin { get; set; }

        /// <summary>
        /// Used to indicate a username/password challenge was already performed
        /// </summary>
        public Guid? TemporaryTwoFactorLoginToken { get; set; }

        /// <summary>
        /// A <see cref="TemporaryTwoFactorLoginToken"/> is issued and will expire after this time.
        /// </summary>
        public DateTime? DateTemporaryTwoFactorLoginTokenExpiresUtc { get; set; }

        /// <summary>
        /// True to require password change on next login
        /// </summary>
        public bool RequirePasswordResetOnLogin { get; set; }

        /// <summary>
        /// The password complexity level on the current password
        /// </summary>
        public short PasswordComplexityLevel { get; set; }

        /// <summary>
        /// Token used to verify email address
        /// </summary>
        public Guid EmailConfirmationToken { get; set; }

        /// <summary>
        /// Date email confirmation token expires
        /// </summary>
        public DateTime? DateEmailConfirmationTokenExpiresUtc { get; set; }

        /// <summary>
        /// Date email was confirmed
        /// </summary>
        public DateTime? DateEmailConfirmedUtc { get; set; }

        [Required]
        public DateTime DateCreatedUtc { get; set; }

        [Required]
        public DateTime DateModifiedUtc { get; set; }

        /// <summary>
        /// IP Address during record creation
        /// </summary>
        public long IP { get; set; }

        [NotMapped]
        public string FirstName
        {
            get
            {
                var parts = Name.Split(new string[] { " " }, StringSplitOptions.None);
                if (parts.Length > 0)
                    return parts[0];
                return Name;
            }
        }

        [NotMapped]
        public string LastName
        {
            get
            {
                var parts = Name.Split(new string[] { " " }, StringSplitOptions.None);
                if (parts.Length > 0)
                    return parts[parts.Length - 1];
                return Name;
            }
        }
    }
}
