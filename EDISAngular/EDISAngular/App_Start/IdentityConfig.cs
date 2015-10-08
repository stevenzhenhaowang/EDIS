using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using EDISAngular.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using EDISAngular.Services;
using EDISAngular.Infrastructure.Authorization.ClaimProviders;


namespace EDISAngular
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }


        /// <summary>
        /// Add additional claim providers here, as all userManager, signInManager will rely on this method to retrieve all claims for persistence.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public override async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);
            identity.AddClaims(AdministrationClaimsProvider.GetClaims(identity));
            identity.AddClaims(AdviserVerificationClaimProvider.GetClaims(identity));

            return identity;
        }


        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };


            manager.EmailService = new DirectToFileMailService();



            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }






    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }


        /// <summary>
        /// This method is called during sign in manager signing in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            var identity = await base.CreateUserIdentityAsync(user);
            return identity;
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

}
