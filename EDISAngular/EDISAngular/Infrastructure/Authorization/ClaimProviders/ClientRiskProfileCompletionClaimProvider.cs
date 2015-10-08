using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EDISAngular;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using EDISAngular.Models;
using EDISAngular.Services;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;

namespace EDISAngular.Infrastructure.Authorization.ClaimProviders
{
    public class ClientRiskProfileCompletionClaimProvider
    {
        public const string ClaimType_clientRiskProfileCompletion = @"http://edis.com.au/claims/client/riskProfile";
        public const string ClaimValue_clientRiskProfile_Completed = "Completed";
        public const string ClaimValue_clientRiskProfile_Incomplete = "Incomplete";

        public async static Task<IEnumerable<Claim>> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
            //Only client account will be processed for this type of claim
            if (user != null && user.IsAuthenticated && user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == AuthorizationRoles.Role_Client))
            {
                using (edisDbEntities db = new edisDbEntities())
                {

                    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var userProfile = await userManager.FindByNameAsync(user.Name);
                    if (userProfile != null)
                    {
                        var profile = db.RiskProfiles.FirstOrDefault(c => c.Client.ClientUserID == userProfile.Id);
                        if (profile != null)
                        {
                            claims.Add(CreateClaim(ClaimType_clientRiskProfileCompletion, ClaimValue_clientRiskProfile_Completed));
                        }
                        else
                        {
                            claims.Add(CreateClaim(ClaimType_clientRiskProfileCompletion, ClaimValue_clientRiskProfile_Incomplete));
                        }
                    }
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