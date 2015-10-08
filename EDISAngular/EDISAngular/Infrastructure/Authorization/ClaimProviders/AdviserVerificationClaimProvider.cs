using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EDISAngular;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Runtime.CompilerServices;
using EDISAngular.Models;
using EDISAngular.Services;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;

namespace EDISAngular.Infrastructure.Authorization.ClaimProviders
{
    public class AdviserVerificationClaimProvider
    {

        /// <summary>
        /// Incomplete implementation, currently will return administration role for every login.
        /// </summary>
        /// <param name="user">User needed to be verified with new claims</param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            List<Claim> claims = new List<Claim>();
            using (edisDbEntities db = new edisDbEntities())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));//HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var userFull = userManager.FindByNameAsync(user.Name).Result;
                var userId = userFull.Id;
                if (db.Advisers.Where(a => a.AdviserId == userId && a.VerifiedId == BusinessLayerParameters.verificationStatus_Verified).Count() > 0)
                {
                    claims.Add(CreateClaim(AuthorizationClaims.ClaimType_VerificationStatus, 
                        AuthorizationClaims.ClaimValue_VerificationStatus_Verified));
                }
            }
            return claims;
        }
        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, AuthorizationProviders.EdisDynamicProvider);
        }
    }
}