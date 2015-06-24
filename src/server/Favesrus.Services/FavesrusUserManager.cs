using Favesrus.DAL;
using Favesrus.Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace Favesrus.Services
{
    public class FavesrusUserManager : UserManager<FavesrusUser>
    {
        public FavesrusUserManager(IUserStore<FavesrusUser> store)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<FavesrusUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<FavesrusUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<FavesrusUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            //TODO: Revisit
            //this.EmailService = emailService;
            //this.SmsService = new SmsService();

            //var dataProtectionProvider = Startup.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

            //    this.UserTokenProvider = new DataProtectorTokenProvider<FavesrusUser>(dataProtector);
            //}

            ////alternatively use this if you are running in Azure Web Sites
            //this.UserTokenProvider = new EmailTokenProvider<FavesrusUser, string>();
        }

        public static FavesrusUserManager Create(
            IdentityFactoryOptions<FavesrusUserManager> options,
            IOwinContext context)
        {
            FavesrusDbContext dbContext = context.Get<FavesrusDbContext>();
            FavesrusUserManager manager =
                new FavesrusUserManager(new UserStore<FavesrusUser>(dbContext));
            return manager;
        }

    }
}
