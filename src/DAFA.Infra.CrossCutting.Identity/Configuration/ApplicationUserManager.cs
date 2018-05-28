using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAFA.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            // Configuring validator for username
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Password's validation and complexity
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Lockout configutation
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Two factor authentication provider
            RegisterTwoFactorProvider("SMS Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your safety code is: {0}"
            });

            RegisterTwoFactorProvider("E-mail Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Safety Code",
                BodyFormat = "Your safety code is: {0}"
            });

            // E-mail service class definition
            EmailService = new EmailService();

            // SMS service class definition
            SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider(nameof(DAFA));
            var dataProtector = provider.Create("ASP.NET Identity");

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);
        }

        /// <summary>
        /// Async login method. This method stores the conected client's data.
        /// </summary>
        /// <param name="user">See <see cref="ApplicationUser"/>.</param>
        /// <param name="clientKey">Client's key.</param>
        /// <returns><see cref="IdentityResult"/> task.</returns>
        public async Task<IdentityResult> SignInClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException(nameof(clientKey));
            }

            var client = user.Clients.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client == null)
            {
                client = new Client { ClientKey = clientKey };
                user.Clients.Add(client);
            }

            var result = await UpdateAsync(user);
            user.CurrentClientId = client.Id.ToString();
            return result;
        }

        /// <summary>
        /// Async login method. This method removes the connected client's data.
        /// </summary>
        /// <param name="user">See <see cref="ApplicationUser"/>.</param>
        /// <param name="clientKey">Client's key.</param>
        /// <returns><see cref="IdentityResult"/> task.</returns>
        public async Task<IdentityResult> SignOutClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException(nameof(clientKey));
            }

            var client = user.Clients.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client != null)
            {
                user.Clients.Remove(client);
            }

            user.CurrentClientId = null;
            return await UpdateAsync(user);
        }
    }
}
